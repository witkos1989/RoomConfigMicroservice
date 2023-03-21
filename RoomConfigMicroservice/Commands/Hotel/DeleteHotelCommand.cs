using MediatR;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.Models;
using System.Diagnostics;

namespace RoomConfigMicroservice.Commands.Hotel;

public record DeleteHotelCommand : IRequest<string>
{
    public string Id { get; init; }
}

public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, string>
{
    private readonly IDatabaseManager _databaseManager;
    private readonly ILogger<DeleteHotelCommandHandler> _logger;

    public DeleteHotelCommandHandler(IDatabaseManager databaseManager,
        ILogger<DeleteHotelCommandHandler> logger)
    {
        _databaseManager = databaseManager;
        _logger = logger;
    }

    public async Task<string> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var hotel = await _databaseManager.Hotel.GetHotelAsync(request.Id, false);

        if (hotel is null)
        {
            return string.Empty;
        }

        _databaseManager.Hotel.RemoveHotel(hotel);

        await _databaseManager.SaveAsync();

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return "Room deleted";
    }
}