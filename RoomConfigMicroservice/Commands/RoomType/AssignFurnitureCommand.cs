using MediatR;
using AutoMapper;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.Models;
using System.Diagnostics;
using RoomConfigMicroservice.DTOs;

namespace RoomConfigMicroservice.Commands.RoomType;

public record AssignFurnitureCommand : IRequest<RoomTypeDTO?>
{
	public string RoomTypeId { get; init; }

	public string FurnitureId { get; init; }
}

public class AssignFurnitureCommandHandler : IRequestHandler<AssignFurnitureCommand, RoomTypeDTO?>
{
    private readonly ILogger<AssignFurnitureCommandHandler> _logger;
    private readonly IDatabaseManager _databaseManager;
    private readonly IMapper _mapper;

    public AssignFurnitureCommandHandler(
        ILogger<AssignFurnitureCommandHandler> logger,
        IDatabaseManager databaseManager,
        IMapper mapper)
    {
        _logger = logger;
        _databaseManager = databaseManager;
        _mapper = mapper;
    }

    public async Task<RoomTypeDTO?> Handle(AssignFurnitureCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var roomType = await _databaseManager.RoomType.GetRoomTypeAsync(request.RoomTypeId, true);

        var furniture = await _databaseManager.Furniture.GetFurnitureAsync(request.FurnitureId, true);

        if (roomType is null || furniture is null)
        {
            return null;
        }

        roomType.Furnitures.Add(furniture);

        await _databaseManager.SaveAsync();

        var roomTypeDTO = _mapper.Map<RoomTypeDTO>(roomType);

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return roomTypeDTO;
    }
}