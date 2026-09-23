// Blittable mirrors of the XSAPI error types -- xsapi-c\errors_c.h, GDK edition 260404.

namespace GDK.Net.Interop;

/// <summary>Mirrors <c>XblErrorCondition</c>.</summary>
internal enum XblErrorCondition : uint
{
    NoError = 0,
    GenericError = 1,
    GenericOutOfRange = 2,
    Auth = 3,
    Network = 4,
    HttpGeneric = 5,
    Http304NotModified = 6,
    Http404NotFound = 7,
    Http412PreconditionFailed = 8,
    Http429TooManyRequests = 9,
    HttpServiceTimeout = 10,
    Rta = 11,
}
