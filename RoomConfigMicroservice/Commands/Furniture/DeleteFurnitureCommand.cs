using System.Diagnostics;
using MediatR;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.Services;

namespace RoomConfigMicroservice.Commands.Furniture;

public record DeleteFurnitureCommand : IRequest<string>
{
    public string Id { get; init; }
}

public class DeleteFurnitureCommandHandler : IRequestHandler<DeleteFurnitureCommand, string>
{
    private readonly ILogger<DeleteFurnitureCommandHandler> _logger;
    private readonly IDatabaseManager _databaseManager;

    public DeleteFurnitureCommandHandler(
        ILogger<DeleteFurnitureCommandHandler> logger,
        IDatabaseManager databaseManager)
    {
        _logger = logger;
        _databaseManager = databaseManager;
    }

    public async Task<string> Handle(DeleteFurnitureCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var furniture = await _databaseManager.Furniture.GetFurnitureAsync(request.Id, true);

        if (furniture is null)
        {
            return string.Empty;
        }

        _databaseManager.Furniture.RemoveFurniture(furniture);

        await _databaseManager.SaveAsync();

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return "Furniture deleted";
    }
}