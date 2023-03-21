using System.Diagnostics;
using MediatR;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.Services;

namespace RoomConfigMicroservice.Commands.Furniture;

public record GetFurnituresCommand :IRequest<IEnumerable<Models.Furniture>>
{ }

public class GetFurnituresCommandHandler : IRequestHandler<GetFurnituresCommand, IEnumerable<Models.Furniture>>
{
    private readonly ILogger<GetFurnituresCommandHandler> _logger;
    private readonly IDatabaseManager _databaseManager;
    
    public GetFurnituresCommandHandler(
        ILogger<GetFurnituresCommandHandler> logger,
        IDatabaseManager databaseManager)
    {
        _logger = logger;
        _databaseManager = databaseManager;
    }

    public async Task<IEnumerable<Models.Furniture>> Handle(GetFurnituresCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var furnitures = await _databaseManager.Furniture.GetAllFurnituresAsync(false);

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return furnitures;
    }
}