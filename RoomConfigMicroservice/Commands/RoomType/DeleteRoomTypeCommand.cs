using MediatR;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.Models;
using System.Diagnostics;

namespace RoomConfigMicroservice.Commands.RoomType;

public record DeleteRoomTypeCommand : IRequest<string>
{
    public string Id { get; init; }
}

public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, string>
{
    private readonly ILogger<DeleteRoomTypeCommandHandler> _logger;
    private readonly IDatabaseManager _databaseManager;

    public DeleteRoomTypeCommandHandler(
        ILogger<DeleteRoomTypeCommandHandler> logger,
        IDatabaseManager databaseManager)
    {
        _logger = logger;
        _databaseManager = databaseManager;
    }

    public async Task<string> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var roomType = await _databaseManager.RoomType.GetRoomTypeAsync(request.Id, false);

        if (roomType is null)
        {
            return string.Empty;
        }

        _databaseManager.RoomType.RemoveRoomType(roomType);

        await _databaseManager.SaveAsync();

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return "Furniture deleted";
    }
}