using MediatR;
using AutoMapper;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.Models;
using System.Diagnostics;
using RoomConfigMicroservice.DTOs;

namespace RoomConfigMicroservice.Commands.Hotel;

public record AssignRoomCommand : IRequest<HotelDTO?>
{
	public string HotelId { get; init; }

	public string RoomId { get; init; }
}

public class AssignRoomCommandHandler : IRequestHandler<AssignRoomCommand, HotelDTO?>
{
    private readonly IDatabaseManager _databaseManager;
    private readonly ILogger<AssignRoomCommandHandler> _logger;
    private readonly IMapper _mapper;

    public AssignRoomCommandHandler(IDatabaseManager databaseManager,
        ILogger<AssignRoomCommandHandler> logger,
        IMapper mapper)
    {
        _databaseManager = databaseManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<HotelDTO?> Handle(AssignRoomCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var hotel = await _databaseManager.Hotel.GetHotelAsync(request.HotelId, true);

        var room = await _databaseManager.Room.GetRoomAsync(request.RoomId, true);

        if (hotel is null || room is null)
        {
            return null;
        }

        hotel.Rooms.Add(room);

        await _databaseManager.SaveAsync();

        var hotelDTO = _mapper.Map<HotelDTO>(hotel);

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return hotelDTO;
    }
}