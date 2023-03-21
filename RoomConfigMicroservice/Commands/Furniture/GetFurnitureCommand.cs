using System.Diagnostics;
using MediatR;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.Persistence;
using RoomConfigMicroservice.Services;

namespace RoomConfigMicroservice.Commands.Furniture;

public record GetFurnitureCommand : IRequest<Models.Furniture?>
{
    public string Id { get; init; }
}

public class GetFurnitureCommandHandler : IRequestHandler<GetFurnitureCommand, Models.Furniture?>
{
    private readonly ILogger<GetFurnitureCommandHandler> _logger;
    private readonly IDatabaseManager _databaseManager;

    public GetFurnitureCommandHandler(
        ILogger<GetFurnitureCommandHandler> logger,
        IDatabaseManager databaseManager)
    {
        _logger = logger;
        _databaseManager = databaseManager;
    }

    public async Task<Models.Furniture?> Handle(GetFurnitureCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var furniture = await _databaseManager.Furniture.GetFurnitureAsync(request.Id, false);

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return furniture;
    }
}