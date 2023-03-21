using MediatR;
using AutoMapper;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.Models;
using System.Diagnostics;

namespace RoomConfigMicroservice.Commands.Hotel;

public record CreateHotelCommand :IRequest<string>
{
    public string Name { get; init; }

    public string Description { get; init; }
}

public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, string>
{
    private readonly IDatabaseManager _databaseManager;
    private readonly ILogger<CreateHotelCommandHandler> _logger;
    private readonly IMapper _mapper;

    public CreateHotelCommandHandler(IDatabaseManager databaseManager,
        ILogger<CreateHotelCommandHandler> logger,
        IMapper mapper)
    {
        _databaseManager = databaseManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var hotel = _mapper.Map<Models.Hotel>(request);

        hotel.Id = Guid.NewGuid().ToString();

        try
        {
            await _databaseManager.Hotel.AddHotelAsync(hotel);

            await _databaseManager.SaveAsync();
        }
        catch
        {
            return string.Empty;
        }


        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return hotel.Id;
    }
}
