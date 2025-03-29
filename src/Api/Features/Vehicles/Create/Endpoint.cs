using FastEndpoints;

namespace Api.Features.Vehicles.Create;

internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Post("api/vehicles");
        Description(x =>
            x.Produces<Response>(StatusCodes.Status201Created, "Vehicle created successfully!")
        );
    }

    public override Task HandleAsync(Request req, CancellationToken ct) =>
        SendOkAsync(Response, ct);
}
