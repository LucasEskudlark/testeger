using System.Net;

namespace Testeger.Application.Exceptions.Mappings;

public static class ExceptionStatusCodeMapping
{
    public static readonly Dictionary<Type, HttpStatusCode> ExceptionToStatusCode = new()
{
    { typeof(NotFoundException), HttpStatusCode.NotFound }
};
}