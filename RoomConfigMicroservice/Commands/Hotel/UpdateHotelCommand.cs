using AutoMapper;
using MediatR;
using RoomConfigMicroservice.Services;
using System.Diagnostics;

namespace RoomConfigMicroservice.Commands.Hotel;

public record UpdateHotelCommand : IRequest<string>
{
    public string Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }
}

public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, string>
{
    private readonly IDatabaseManager _databaseManager;
    private readonly ILogger<UpdateHotelCommandHandler> _logger;
    private readonly IMapper _mapper;

    public UpdateHotelCommandHandler(IDatabaseManager databaseManager,
        ILogger<UpdateHotelCommandHandler> logger,
        IMapper mapper)
    {
        _databaseManager = databaseManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<string> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var hotel = await _databaseManager.Hotel.GetHotelAsync(request.Id, false);

        if (hotel is null)
        {
            return string.Empty;
        }

        hotel = _mapper.Map<Models.Hotel>(request);

        _databaseManager.Hotel.UpdateHotel(hotel);

        await _databaseManager.SaveAsync();

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return hotel.Id;
    }
}
