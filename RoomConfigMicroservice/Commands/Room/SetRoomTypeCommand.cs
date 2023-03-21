using AutoMapper;
using MediatR;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.DTOs;
using System.Diagnostics;

namespace RoomConfigMicroservice.Commands.Room;

public record SetRoomTypeCommand : IRequest<RoomDTO?>
{
	public string RoomId { get; init; }

	public string RoomTypeId { get; init; }
}

public class SetRoomTypeCommandHandler : IRequestHandler<SetRoomTypeCommand, RoomDTO?>
{
    private readonly ILogger<SetRoomTypeCommandHandler> _logger;
    private readonly IDatabaseManager _databaseManager;
    private readonly IMapper _mapper;

    public SetRoomTypeCommandHandler(
        ILogger<SetRoomTypeCommandHandler> logger,
        IDatabaseManager databaseManager,
        IMapper mapper)
    {
        _logger = logger;
        _databaseManager = databaseManager;
        _mapper = mapper;
    }

    public async Task<RoomDTO?> Handle(SetRoomTypeCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var room = await _databaseManager.Room.GetRoomAsync(request.RoomId, true);

        var roomType = await _databaseManager.RoomType.GetRoomTypeAsync(request.RoomTypeId, true);

        if (room is null || roomType is null)
        {
            return null;
        }

        room.RoomType = roomType;

        await _databaseManager.SaveAsync();

        var roomDTO = _mapper.Map<RoomDTO>(room);

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return roomDTO;
    }
}