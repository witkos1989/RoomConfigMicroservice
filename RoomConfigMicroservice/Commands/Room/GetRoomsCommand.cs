using MediatR;
using AutoMapper;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.DTOs;
using System.Diagnostics;

namespace RoomConfigMicroservice.Commands.Room;

public record GetRoomsCommand : IRequest<IEnumerable<RoomDTO>>
{ }

public class GetRoomsCommandHandler : IRequestHandler<GetRoomsCommand, IEnumerable<RoomDTO>>
{
    private readonly ILogger<GetRoomsCommandHandler> _logger;
    private readonly IDatabaseManager _databaseManager;
    private readonly IMapper _mapper;

    public GetRoomsCommandHandler(
        ILogger<GetRoomsCommandHandler> logger,
        IDatabaseManager databaseManager,
        IMapper mapper)
    {
        _logger = logger;
        _databaseManager = databaseManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoomDTO>> Handle(GetRoomsCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var rooms = await _databaseManager.Room.GetAllRoomsAsync(false);

        var roomsDTO = _mapper.Map<ICollection<RoomDTO>>(rooms);

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return roomsDTO;
    }
}