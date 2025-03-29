using FastEndpoints;

namespace Api.Features.Vehicles.Create;

internal sealed class Request { }

internal sealed class Validator : Validator<Request>
{
    public Validator() { }
}

internal sealed class Response
{
    public static string Message => "This endpoint hasn't been implemented yet!";
}
