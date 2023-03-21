using MediatR;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.Models;
using System.Diagnostics;

namespace RoomConfigMicroservice.Commands.Room;

public record DeleteRoomCommand : IRequest<string>
{
	public string Id { get; init; }
}

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, string>
{
    private readonly ILogger<DeleteRoomCommandHandler> _logger;
    private readonly IDatabaseManager _databaseManager;

    public DeleteRoomCommandHandler(
        ILogger<DeleteRoomCommandHandler> logger,
        IDatabaseManager databaseManager)
    {
        _logger = logger;
        _databaseManager = databaseManager;
    }

    public async Task<string> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var room = await _databaseManager.Room.GetRoomAsync(request.Id, false);

        if (room is null)
        {
            return string.Empty;
        }

        _databaseManager.Room.RemoveRoom(room);

        await _databaseManager.SaveAsync();

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return "Room deleted";
    }
}