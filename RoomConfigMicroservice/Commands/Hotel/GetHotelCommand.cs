using AutoMapper;
using MediatR;
using System.Diagnostics;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.DTOs;

namespace RoomConfigMicroservice.Commands.Hotel;

public record GetHotelCommand :IRequest<HotelDTO?>
{
	public string Id { get; init; }
}

public class GetHotelCommandHandler : IRequestHandler<GetHotelCommand, HotelDTO?>
{
    private readonly IDatabaseManager _databaseManager;
    private readonly ILogger<GetHotelCommandHandler> _logger;
    private readonly IMapper _mapper;

    public GetHotelCommandHandler(IDatabaseManager databaseManager,
        ILogger<GetHotelCommandHandler> logger,
        IMapper mapper)
    {
        _databaseManager = databaseManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<HotelDTO?> Handle(GetHotelCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var hotel = await _databaseManager.Hotel.GetHotelAsync(request.Id, false);

        if (hotel is null)
        {
            return null;
        }

        var hotelDTO = _mapper.Map<HotelDTO>(hotel);

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return hotelDTO;
    }
}