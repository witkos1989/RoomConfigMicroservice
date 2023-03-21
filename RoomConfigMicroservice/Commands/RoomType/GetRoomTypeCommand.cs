using MediatR;
using AutoMapper;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.DTOs;
using System.Diagnostics;

namespace RoomConfigMicroservice.Commands.RoomType;

public record GetRoomTypeCommand : IRequest<RoomTypeDTO?>
{
	public string Id { get; init; }
}

public class GetRoomTypeCommandHandler : IRequestHandler<GetRoomTypeCommand, RoomTypeDTO?>
{
    private readonly ILogger<GetRoomTypeCommandHandler> _logger;
    private readonly IDatabaseManager _databaseManager;
    private readonly IMapper _mapper;

    public GetRoomTypeCommandHandler(
        ILogger<GetRoomTypeCommandHandler> logger,
        IDatabaseManager databaseManager,
        IMapper mapper)
    {
        _logger = logger;
        _databaseManager = databaseManager;
        _mapper = mapper;
    }

    public async Task<RoomTypeDTO?> Handle(GetRoomTypeCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var roomType = await _databaseManager.RoomType.GetRoomTypeAsync(request.Id, false);

        if (roomType is null)
        {
            return null;
        }

        var roomTypeDTO = _mapper.Map<RoomTypeDTO>(roomType);

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return roomTypeDTO;
    }
}