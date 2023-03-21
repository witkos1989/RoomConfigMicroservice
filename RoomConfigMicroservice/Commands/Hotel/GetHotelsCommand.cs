using AutoMapper;
using MediatR;
using System.Diagnostics;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.DTOs;

namespace RoomConfigMicroservice.Commands.Hotel;

public record GetHotelsCommand : IRequest<IEnumerable<HotelDTO>>
{ }

public class GetHotelsCommandHandler : IRequestHandler<GetHotelsCommand, IEnumerable<HotelDTO>>
{
    private readonly IDatabaseManager _databaseManager;
    private readonly ILogger<GetHotelsCommandHandler> _logger;
    private readonly IMapper _mapper;

    public GetHotelsCommandHandler(IDatabaseManager databaseManager,
        ILogger<GetHotelsCommandHandler> logger,
        IMapper mapper)
    {
        _databaseManager = databaseManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<HotelDTO>> Handle(GetHotelsCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var hotels = await _databaseManager.Hotel.GetAllHotelsAsync(false);

        var hotelsDTO = _mapper.Map<IEnumerable<HotelDTO>>(hotels);

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return hotelsDTO;
    }
}