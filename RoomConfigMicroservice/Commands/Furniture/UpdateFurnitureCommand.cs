using MediatR;
using AutoMapper;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.Services;
using System.Diagnostics;

namespace RoomConfigMicroservice.Commands.Furniture;

public record UpdateFurnitureCommand : IRequest<string>
{
	public string Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }
}

public class UpdateFurnitureCommandHandler : IRequestHandler<UpdateFurnitureCommand, string>
{
    private readonly ILogger<UpdateFurnitureCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IDatabaseManager _databaseManager;

    public UpdateFurnitureCommandHandler(
        ILogger<UpdateFurnitureCommandHandler> logger,
        IMapper mapper,
        IDatabaseManager databaseManager)
    {
        _logger = logger;
        _mapper = mapper;
        _databaseManager = databaseManager;
    }

    public async Task<string> Handle(UpdateFurnitureCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var furniture = await _databaseManager.Furniture.GetFurnitureAsync(request.Id, false);

        if (furniture is null)
        {
            return string.Empty;
        }

        furniture = _mapper.Map<Models.Furniture>(request);

        _databaseManager.Furniture.UpdateFurniture(furniture);

        await _databaseManager.SaveAsync();

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return furniture.Id;
    }
}