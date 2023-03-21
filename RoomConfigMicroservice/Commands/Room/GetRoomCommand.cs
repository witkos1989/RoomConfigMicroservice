using MediatR;
using AutoMapper;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.DTOs;
using System.Diagnostics;

namespace RoomConfigMicroservice.Commands.Room;

public record GetRoomCommand : IRequest<RoomDTO?>
{
	public string Id { get; init; }
}

public class GetRoomCommandHandler : IRequestHandler<GetRoomCommand, RoomDTO?>
{
    private readonly ILogger<GetRoomCommandHandler> _logger;
    private readonly IDatabaseManager _databaseManager;
    private readonly IMapper _mapper;

    public GetRoomCommandHandler(
        ILogger<GetRoomCommandHandler> logger,
        IDatabaseManager databaseManager,
        IMapper mapper)
    {
        _logger = logger;
        _databaseManager = databaseManager;
        _mapper = mapper;
    }

    public async Task<RoomDTO?> Handle(GetRoomCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var room = await _databaseManager.Room.GetRoomAsync(request.Id ,false);

        if (room is null)
        {
            return null;
        }

        var roomDTO = _mapper.Map<RoomDTO>(room);

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return roomDTO;
    }
}