using MediatR;
using AutoMapper;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.Services;
using System.Diagnostics;

namespace RoomConfigMicroservice.Commands.Furniture;

public record CreateFurnitureCommand : IRequest<string>
{
    public string Name { get; init; }

    public string Description { get; init; }
}

public class CreateFurnitureCommandHandler : IRequestHandler<CreateFurnitureCommand, string>
{
    private readonly ILogger<CreateFurnitureCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IDatabaseManager _databaseManager;

    public CreateFurnitureCommandHandler(
        ILogger<CreateFurnitureCommandHandler> logger,
        IMapper mapper,
        IDatabaseManager databaseManager)
    {
        _logger = logger;
        _mapper = mapper;
        _databaseManager = databaseManager;
    }

    public async Task<string> Handle(CreateFurnitureCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var furniture = _mapper.Map<Models.Furniture>(request);

        furniture.Id = Guid.NewGuid().ToString();

        try
        {
            await _databaseManager.Furniture.AddFurnitureAsync(furniture);

            await _databaseManager.SaveAsync();
        }
        catch
        {
            return string.Empty;
        }

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return furniture.Id;
    }
}