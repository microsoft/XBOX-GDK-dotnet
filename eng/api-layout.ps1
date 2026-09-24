<#
.SYNOPSIS
    Lays the generated API reference out one folder per namespace, and writes the folder indexes.

.DESCRIPTION
    docfx writes every page into a single flat directory. The projection has more than 1,100 public
    types, and GitHub stops rendering a directory listing past 1,000 entries, so a flat docs/api
    silently hides pages from anyone browsing the repository on the web.

    This script moves each page into a folder named for its namespace, rewrites the relative links
    between pages, and writes a README.md index into every folder plus the root. It is dot-sourced
    by eng/generate-docs.ps1 and can also be run directly against the committed tree.

    The layout is derived from the pages themselves, not from a hard-coded list: a namespace page is
    recognised by its "Namespace <name>" heading, and every other page is assigned to the longest
    namespace that prefixes its file name. That keeps nested types with their declaring namespace
    and needs no change when a namespace is added.

    Running it twice is a no-op, so it is safe to re-run over an already converted tree.

.PARAMETER Path
    The API reference directory to lay out. Defaults to docs/api.

.PARAMETER PassThru
    Emit the namespace-to-folder map.
#>
[CmdletBinding()]
param(
    [string] $Path,
    [switch] $PassThru
)

Set-StrictMode -Version Latest

$script:ApiLayoutRoot = Split-Path -Parent $PSScriptRoot

function Get-ApiAreaMetadata {
    <#
    .SYNOPSIS
        Reads eng/api-areas.json and eng/gdk-learn-index.json.
    #>
    $areasPath = Join-Path $PSScriptRoot 'api-areas.json'
    $indexPath = Join-Path $PSScriptRoot 'gdk-learn-index.json'

    if (-not (Test-Path $areasPath)) { throw "Missing $areasPath." }
    if (-not (Test-Path $indexPath)) { throw "Missing $indexPath." }

    return [pscustomobject]@{
        Areas = (Get-Content $areasPath -Raw | ConvertFrom-Json).areas
        Learn = Get-Content $indexPath -Raw | ConvertFrom-Json
    }
}

function Get-LearnUrl {
    <#
    .SYNOPSIS
        Resolves a native GDK symbol or API family to its Microsoft Learn URL.
    #>
    param(
        [Parameter(Mandatory)] $Learn,
        [Parameter(Mandatory)] [string] $Name,
        [switch] $Family
    )

    $table = if ($Family) { $Learn.families } else { $Learn.symbols }
    $property = $table.PSObject.Properties[$Name]
    if (-not $property) { return $null }

    return "$($Learn.base)$($property.Value)?view=$($Learn.view)"
}

function Get-ApiNamespaces {
    <#
    .SYNOPSIS
        Returns the namespaces the reference documents, newest layout or flat.
    #>
    param([Parameter(Mandatory)] [string] $Directory)

    $namespaces = [System.Collections.Generic.List[string]]::new()
    foreach ($file in Get-ChildItem $Directory -Filter '*.md' -File -Recurse) {
        if ($file.Name -eq 'README.md') { continue }

        # A namespace page opens with: # <a id="..."></a> Namespace <name>
        $heading = Get-Content $file.FullName -TotalCount 1
        if ($heading -match '^#\s+<a id="[^"]*"></a>\s+Namespace\s+(\S+)\s*$') {
            $namespaces.Add($Matches[1])
        }
    }

    if ($namespaces.Count -eq 0) {
        throw "No namespace pages found under $Directory. Did docfx produce any output?"
    }

    return $namespaces | Sort-Object
}

function Get-NamespaceFolder {
    <#
    .SYNOPSIS
        Maps a namespace to its folder, relative to the reference root.
    #>
    param(
        [Parameter(Mandatory)] [string] $Namespace,
        [Parameter(Mandatory)] $Areas
    )

    $area = $Areas.PSObject.Properties[$Namespace]
    if ($area) { return $area.Value.folder }

    # A namespace with no entry in api-areas.json still gets a home, so adding one to the
    # projection never loses its pages. Add the metadata to give it a described index.
    if ($Namespace -eq 'GDK.Net') { return 'Core' }
    if ($Namespace -like 'GDK.Net.*') {
        return ($Namespace.Substring('GDK.Net.'.Length) -replace '\.', '/')
    }

    return ($Namespace -replace '\.', '/')
}

function Resolve-PageNamespace {
    <#
    .SYNOPSIS
        Assigns a page to the longest namespace that prefixes its file name.
    #>
    param(
        [Parameter(Mandatory)] [string] $PageName,
        [Parameter(Mandatory)] [string[]] $Namespaces
    )

    $best = $null
    foreach ($namespace in $Namespaces) {
        if ($PageName -eq $namespace -or $PageName.StartsWith("$namespace.", [System.StringComparison]::Ordinal)) {
            if ($null -eq $best -or $namespace.Length -gt $best.Length) { $best = $namespace }
        }
    }

    return $best
}

function ConvertTo-IndexText {
    <#
    .SYNOPSIS
        Flattens a docfx summary into a single table cell.
    #>
    param([string] $Text)

    if ([string]::IsNullOrWhiteSpace($Text)) { return '' }

    # docfx emits unresolved cross-references as <xref href="Full.Type.Name" ...></xref>; the
    # trailing segment is what a reader recognises.
    $Text = [regex]::Replace($Text, '<xref href="([^"]+)"[^>]*>\s*</xref>', {
        param($match)
        $uid = $match.Groups[1].Value -replace '\*$', ''
        '`' + ($uid -split '\.')[-1] + '`'
    })

    $Text = $Text -replace '</?code>', '`'
    $Text = $Text -replace '<[^>]+>', ''
    $Text = $Text -replace '\s+', ' '
    $Text = $Text -replace '\|', '\|'

    return $Text.Trim()
}

function Get-NamespaceMembers {
    <#
    .SYNOPSIS
        Parses a namespace page into its type rows: name, kind, page and summary.
    #>
    param([Parameter(Mandatory)] [string] $NamespacePage)

    $members = [System.Collections.Generic.List[pscustomobject]]::new()
    $kind = $null
    $current = $null
    $summary = [System.Text.StringBuilder]::new()

    # docfx groups a namespace page by type kind; the heading is the plural.
    $kinds = @{
        'Classes'    = 'Class'
        'Structs'    = 'Struct'
        'Enums'      = 'Enum'
        'Delegates'  = 'Delegate'
        'Interfaces' = 'Interface'
    }

    $complete = {
        if ($null -ne $current) {
            $current.Summary = ConvertTo-IndexText $summary.ToString()
            $members.Add($current)
        }
    }

    foreach ($line in Get-Content $NamespacePage) {
        if ($line -match '^###\s+(.+?)\s*$') {
            & $complete
            $current = $null
            [void] $summary.Clear()

            # "Namespaces" lists child namespaces, which get their own folder and index.
            $heading = $Matches[1]
            $kind = if ($kinds.ContainsKey($heading)) { $kinds[$heading] } else { $null }
            if (-not $kind -and $heading -ne 'Namespaces') {
                Write-Warning "Unrecognised section '$heading' in $NamespacePage; its types are omitted from the index."
            }
            continue
        }

        if ($null -ne $kind -and $line -match '^\s*\[([^\]]+)\]\(([^)]+\.md)\)\s*$') {
            & $complete
            [void] $summary.Clear()
            $current = [pscustomobject]@{
                Name    = $Matches[1]
                Page    = $Matches[2]
                Kind    = $kind
                Summary = ''
            }
            continue
        }

        if ($null -ne $current) { [void] $summary.AppendLine($line) }
    }

    & $complete
    return $members
}

function Get-RelativeLink {
    <#
    .SYNOPSIS
        Builds a relative markdown link from one folder to a file in another.
    #>
    param(
        [Parameter(Mandatory)] [AllowEmptyString()] [string] $FromFolder,
        [Parameter(Mandatory)] [AllowEmptyString()] [string] $ToFolder,
        [Parameter(Mandatory)] [string] $FileName
    )

    if ($FromFolder -eq $ToFolder) { return $FileName }

    $from = @(if ($FromFolder) { $FromFolder -split '/' })
    $to = @(if ($ToFolder) { $ToFolder -split '/' })

    $common = 0
    while ($common -lt $from.Count -and $common -lt $to.Count -and $from[$common] -eq $to[$common]) {
        $common++
    }

    $segments = [System.Collections.Generic.List[string]]::new()
    for ($i = $common; $i -lt $from.Count; $i++) { $segments.Add('..') }
    for ($i = $common; $i -lt $to.Count; $i++) { $segments.Add($to[$i]) }
    $segments.Add($FileName)

    return ($segments -join '/')
}

function Update-PageLinks {
    <#
    .SYNOPSIS
        Rewrites links between reference pages so they survive the move into folders.
    #>
    param(
        [Parameter(Mandatory)] [string] $File,
        [Parameter(Mandatory)] [AllowEmptyString()] [string] $Folder,
        [Parameter(Mandatory)] [hashtable] $PageFolders
    )

    $original = [System.IO.File]::ReadAllText($File)
    $text = $original

    # docfx resolves the *fields* of a named ValueTuple to Learn URLs such as
    # "system.valuetuple-gdk.net.playfab.multiplayer.operationid,...-.operation", which do not
    # exist and return 404. Keep the field name as text and drop the link.
    $text = [regex]::Replace(
        $text,
        '\[([^\]]+)\]\(https://learn\.microsoft\.com/dotnet/api/system\.valuetuple[^)]*\)',
        '$1')

    # docfx backslash-escapes the fragment separator and every underscore in the anchor
    # ("page.md\#GDK\_Net\_Type\_Member"). CommonMark unescapes those, so the link works,
    # but the pattern has to allow for them or a cross-folder target is never rewritten.
    $rewritten = [regex]::Replace($text, '\]\((?!https?:|#)([^)\s]*?)(\.md)(\\?#[^)\s]*)?\)', {
        param($match)

        $target = $match.Groups[1].Value
        $anchor = $match.Groups[3].Value
        $leaf = ($target -split '[\\/]')[-1]

        if (-not $PageFolders.ContainsKey($leaf)) {
            # Not a reference page: the root index's links out to the authored guides.
            return $match.Value
        }

        $link = Get-RelativeLink -FromFolder $Folder -ToFolder $PageFolders[$leaf] -FileName "$leaf.md"
        return "]($link$anchor)"
    })

    if ($rewritten -ne $original) {
        [System.IO.File]::WriteAllText($File, $rewritten, [System.Text.UTF8Encoding]::new($false))
    }
}

function Write-AreaIndex {
    <#
    .SYNOPSIS
        Writes the README.md index for one namespace folder.
    #>
    param(
        [Parameter(Mandatory)] [string] $Root,
        [Parameter(Mandatory)] [string] $Namespace,
        [Parameter(Mandatory)] [AllowEmptyString()] [string] $Folder,
        [Parameter(Mandatory)] $Metadata,
        [Parameter(Mandatory)] [hashtable] $NamespaceFolders
    )

    $area = $Metadata.Areas.PSObject.Properties[$Namespace]
    $title = if ($area) { $area.Value.title } else { $Namespace }
    $summary = if ($area) { $area.Value.summary } else { "Reference pages for the ``$Namespace`` namespace." }

    $directory = if ($Folder) { Join-Path $Root ($Folder -replace '/', '\') } else { $Root }
    $namespacePage = Join-Path $directory "$Namespace.md"
    if (-not (Test-Path $namespacePage)) {
        throw "Namespace page for $Namespace is missing from $directory."
    }

    $members = Get-NamespaceMembers -NamespacePage $namespacePage
    $depth = if ($Folder) { ($Folder -split '/').Count } else { 0 }
    $up = if ($depth -gt 0) { ('../' * $depth) } else { './' }

    $lines = [System.Collections.Generic.List[string]]::new()
    $lines.Add("# $title")
    $lines.Add('')
    $lines.Add($summary)
    $lines.Add('')
    $lines.Add("Namespace ``$Namespace``. Generated by [``eng/generate-docs.ps1``]($($up)../../eng/generate-docs.ps1) " +
               "from the XML documentation comments in ``src/GDK.Net``. **Do not edit these files by hand.**")
    $lines.Add('')

    # The GDK families this namespace projects, so a reader can cross over to the native reference.
    $familyLinks = [System.Collections.Generic.List[string]]::new()
    if ($area -and $area.Value.PSObject.Properties['families']) {
        foreach ($family in $area.Value.families) {
            $url = Get-LearnUrl -Learn $Metadata.Learn -Name $family -Family
            $familyLinks.Add($(if ($url) { "[``$family``]($url)" } else { "``$family``" }))
        }
    }
    if ($area -and $area.Value.PSObject.Properties['links']) {
        foreach ($link in $area.Value.links) { $familyLinks.Add("[$($link.text)]($($link.url))") }
    }

    if ($familyLinks.Count -gt 0) {
        $lines.Add("**GDK reference:** $($familyLinks -join ' · ')")
        $lines.Add('')
    }

    # Child namespaces, when this namespace has any.
    $children = $NamespaceFolders.Keys |
        Where-Object { $_ -ne $Namespace -and $_.StartsWith("$Namespace.", [System.StringComparison]::Ordinal) } |
        Where-Object { -not $_.Substring($Namespace.Length + 1).Contains('.') } |
        Sort-Object

    if ($children) {
        $lines.Add('## Child namespaces')
        $lines.Add('')
        foreach ($child in $children) {
            $childFolder = $NamespaceFolders[$child]
            $relative = Get-RelativeLink -FromFolder $Folder -ToFolder $childFolder -FileName 'README.md'
            $lines.Add("- [``$child``]($relative)")
        }
        $lines.Add('')
    }

    $lines.Add('## APIs')
    $lines.Add('')

    # Used when a type's doc comment names no resolvable native symbol.
    $fallback = ''
    if ($familyLinks.Count -gt 0) { $fallback = $familyLinks[0] }

    if ($members.Count -eq 0) {
        $lines.Add('This namespace has no public types.')
    }
    else {
        $lines.Add("| API | Kind | Description | GDK reference |")
        $lines.Add("|---|---|---|---|")

        foreach ($member in $members | Sort-Object Kind, Name) {
            $page = ($member.Page -split '[\\/]')[-1]

            # Native symbols the doc comment names, in first-mention order, deduplicated.
            $symbols = [System.Collections.Generic.List[string]]::new()
            foreach ($match in [regex]::Matches($member.Summary, '`((?:Xbl|PF|Party|Chat|X)[A-Za-z0-9_]{3,})`')) {
                $symbol = $match.Groups[1].Value
                if (-not $symbols.Contains($symbol)) { $symbols.Add($symbol) }
            }

            $references = [System.Collections.Generic.List[string]]::new()
            foreach ($symbol in $symbols) {
                $url = Get-LearnUrl -Learn $Metadata.Learn -Name $symbol
                if ($url) { $references.Add("[``$symbol``]($url)") }
                if ($references.Count -ge 3) { break }
            }

            # No named symbol resolved, so fall back to the family the area projects. The link text
            # is the family name, which makes clear it is the family page and not the type's own.
            if ($references.Count -eq 0 -and $fallback) { $references.Add($fallback) }

            $reference = if ($references.Count -gt 0) { $references -join '<br>' } else { '' }
            $description = if ($member.Summary) { $member.Summary } else { '' }
            $lines.Add("| [``$($member.Name)``]($page) | $($member.Kind) | $description | $reference |")
        }
    }

    $lines.Add('')
    $lines.Add('## Related')
    $lines.Add('')
    $lines.Add("- [API reference index]($($up)README.md)")
    $lines.Add("- [Namespace page]($Namespace.md)")
    $lines.Add("- [Getting started]($($up)../getting-started.md)")
    $lines.Add("- [Architecture]($($up)../architecture.md)")

    $path = Join-Path $directory 'README.md'
    [System.IO.File]::WriteAllText($path, (($lines -join "`n") + "`n"), [System.Text.UTF8Encoding]::new($false))
}

function Write-ApiRootIndex {
    <#
    .SYNOPSIS
        Writes docs/api/README.md: the per-area landing table.
    #>
    param(
        [Parameter(Mandatory)] [string] $Root,
        [Parameter(Mandatory)] $Metadata,
        [Parameter(Mandatory)] [hashtable] $NamespaceFolders
    )

    $props = Join-Path $script:ApiLayoutRoot 'Directory.Build.props'
    $match = Select-String -Path $props -Pattern '<GdkEdition>(\d+)</GdkEdition>' | Select-Object -First 1
    if (-not $match) { throw "Could not read <GdkEdition> from $props." }
    $edition = $match.Matches[0].Groups[1].Value

    $pages = @(Get-ChildItem $Root -Filter '*.md' -File -Recurse | Where-Object { $_.Name -ne 'README.md' }).Count

    $lines = [System.Collections.Generic.List[string]]::new()
    $lines.Add('# XBOX GDK.NET API reference')
    $lines.Add('')
    $lines.Add('Every public type and member of the projection, generated from the XML documentation comments in')
    $lines.Add('`src/GDK.Net` by [`eng/generate-docs.ps1`](../../eng/generate-docs.ps1). **Do not edit these files by')
    $lines.Add('hand.** Edit the doc comments and regenerate.')
    $lines.Add('')
    $lines.Add('| | |')
    $lines.Add('|---|---|')
    $lines.Add("| GDK edition | ``$edition`` |")
    $lines.Add('| Target framework | `net10.0` |')
    $lines.Add("| Namespaces | $($NamespaceFolders.Count) |")
    $lines.Add("| Pages | $pages |")
    $lines.Add('')
    $lines.Add('## Layout')
    $lines.Add('')
    $lines.Add('Each namespace has its own folder, and each folder has a `README.md` describing every API in it')
    $lines.Add('and linking to the matching GDK reference on Microsoft Learn. The pages are split this way because')
    $lines.Add('GitHub stops rendering a directory listing past 1,000 entries, and the projection has more pages')
    $lines.Add('than that; a single flat folder would hide the overflow from anyone browsing the repository.')
    $lines.Add('')
    $lines.Add('## Areas')
    $lines.Add('')
    $lines.Add('| Area | Namespace | Covers | APIs |')
    $lines.Add('|---|---|---|---|')

    foreach ($namespace in $NamespaceFolders.Keys | Sort-Object) {
        $folder = $NamespaceFolders[$namespace]
        $area = $Metadata.Areas.PSObject.Properties[$namespace]
        $title = if ($area) { $area.Value.title } else { $namespace }
        $summary = if ($area) { $area.Value.summary } else { '' }

        $directory = if ($folder) { Join-Path $Root ($folder -replace '/', '\') } else { $Root }
        $count = @(Get-ChildItem $directory -Filter '*.md' -File |
            Where-Object { $_.Name -ne 'README.md' -and $_.BaseName -ne $namespace }).Count

        $lines.Add("| [$title]($folder/README.md) | ``$namespace`` | $summary | $count |")
    }

    $lines.Add('')
    $lines.Add('## Scope')
    $lines.Add('')
    $lines.Add('Three things are worth knowing about the scope of this reference.')
    $lines.Add('')
    $lines.Add("**Internal interop is excluded.** docfx's default API filter emits only public and protected")
    $lines.Add('members, so the `GDK.Net.Interop` layer, which holds the raw P/Invokes and the blittable native')
    $lines.Add('structs, does not appear. That layer is `internal` and is not part of the supported surface. See')
    $lines.Add('[`../architecture.md`](../architecture.md).')
    $lines.Add('')
    $lines.Add('**Generated from `net10.0`, and complete for all targets.** The projection multi-targets `net8.0`,')
    $lines.Add('`net10.0` and `netstandard2.0`. The `#if NET7_0_OR_GREATER` guards choose')
    $lines.Add('`[UnmanagedCallersOnly]` function pointers over `Marshal.GetFunctionPointerForDelegate`, but')
    $lines.Add('everything they switch is `internal`: the three assemblies export the same public surface, so')
    $lines.Add('nothing is missing from this reference. Nothing here is target-specific unless the page says so.')
    $lines.Add('')
    $lines.Add('## Related')
    $lines.Add('')
    $lines.Add('- [Getting started](../getting-started.md)')
    $lines.Add('- [Architecture](../architecture.md)')
    $lines.Add('- [Building](../building.md)')
    $lines.Add('- [The pinned GDK edition](../gdk-edition.md)')

    $path = Join-Path $Root 'README.md'
    [System.IO.File]::WriteAllText($path, (($lines -join "`n") + "`n"), [System.Text.UTF8Encoding]::new($false))
}

function Convert-ApiLayout {
    <#
    .SYNOPSIS
        Lays a generated reference directory out one folder per namespace and writes the indexes.
    #>
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)] [string] $Directory,
        [switch] $PassThru
    )

    $metadata = Get-ApiAreaMetadata
    $namespaces = @(Get-ApiNamespaces -Directory $Directory)

    $namespaceFolders = @{}
    foreach ($namespace in $namespaces) {
        $namespaceFolders[$namespace] = Get-NamespaceFolder -Namespace $namespace -Areas $metadata.Areas
    }

    # Page name -> folder, so links can be rewritten wherever the page ends up.
    $pageFolders = @{}
    $moves = [System.Collections.Generic.List[pscustomobject]]::new()

    foreach ($file in Get-ChildItem $Directory -Filter '*.md' -File -Recurse) {
        if ($file.Name -eq 'README.md') { continue }

        $namespace = Resolve-PageNamespace -PageName $file.BaseName -Namespaces $namespaces
        if (-not $namespace) {
            Write-Warning "$($file.Name) matches no namespace; leaving it at the reference root."
            $pageFolders[$file.BaseName] = ''
            continue
        }

        $folder = $namespaceFolders[$namespace]
        $pageFolders[$file.BaseName] = $folder
        $moves.Add([pscustomobject]@{ File = $file; Folder = $folder })
    }

    foreach ($move in $moves) {
        $destination = Join-Path $Directory ($move.Folder -replace '/', '\')
        if (-not (Test-Path $destination)) { New-Item -ItemType Directory -Path $destination -Force | Out-Null }

        $target = Join-Path $destination $move.File.Name
        if ($move.File.FullName -ne $target) { Move-Item -LiteralPath $move.File.FullName -Destination $target -Force }
    }

    # toc.yml stays at the root and its hrefs have to follow the pages.
    $toc = Join-Path $Directory 'toc.yml'
    if (Test-Path $toc) {
        $text = [System.IO.File]::ReadAllText($toc)
        $text = [regex]::Replace($text, '(?m)^(\s*href:\s*)([^\s]+)\.md\s*$', {
            param($match)
            $leaf = ($match.Groups[2].Value -split '[\\/]')[-1]
            if (-not $pageFolders.ContainsKey($leaf)) { return $match.Value }
            $folder = $pageFolders[$leaf]
            $href = if ($folder) { "$folder/$leaf.md" } else { "$leaf.md" }
            "$($match.Groups[1].Value)$href"
        })
        [System.IO.File]::WriteAllText($toc, $text, [System.Text.UTF8Encoding]::new($false))
    }

    foreach ($file in Get-ChildItem $Directory -Filter '*.md' -File -Recurse) {
        if ($file.Name -eq 'README.md') { continue }

        $folder = $pageFolders[$file.BaseName]
        Update-PageLinks -File $file.FullName -Folder $folder -PageFolders $pageFolders
    }

    # Stale folders left behind by a namespace that no longer exists.
    $subfolders = @(Get-ChildItem $Directory -Directory -Recurse) | Sort-Object -Property FullName -Descending
    foreach ($stale in $subfolders) {
        if (-not (Get-ChildItem $stale.FullName -File -Recurse)) {
            Remove-Item $stale.FullName -Recurse -Force
        }
    }

    foreach ($namespace in $namespaces) {
        Write-AreaIndex -Root $Directory -Namespace $namespace -Folder $namespaceFolders[$namespace] `
            -Metadata $metadata -NamespaceFolders $namespaceFolders
    }

    Write-ApiRootIndex -Root $Directory -Metadata $metadata -NamespaceFolders $namespaceFolders

    if ($PassThru) { return $namespaceFolders }
}

# Dot-sourced by eng/generate-docs.ps1, which calls Convert-ApiLayout itself. Running the script
# directly lays out the committed tree, which is what to do after fixing the metadata by hand.
if ($MyInvocation.InvocationName -ne '.') {
    $ErrorActionPreference = 'Stop'
    $target = if ($Path) { $Path } else { Join-Path $script:ApiLayoutRoot 'docs\api' }
    $map = Convert-ApiLayout -Directory $target -PassThru
    Write-Host "docs/api: $($map.Count) namespaces laid out across $(@($map.Values | Sort-Object -Unique).Count) folders."
    if ($PassThru) { $map }
}
