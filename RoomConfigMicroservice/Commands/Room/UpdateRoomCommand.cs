using AutoMapper;
using MediatR;
using RoomConfigMicroservice.Services;
using System.Diagnostics;

namespace RoomConfigMicroservice.Commands.Room;

public record UpdateRoomCommand : IRequest<string>
{
    public string Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public decimal CurrentPrice { get; init; }
}

public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, string>
{
    private readonly ILogger<UpdateRoomCommandHandler> _logger;
    private readonly IDatabaseManager _databaseManager;
    private readonly IMapper _mapper;


    public UpdateRoomCommandHandler(
        ILogger<UpdateRoomCommandHandler> logger,
        IDatabaseManager databaseManager,
        IMapper mapper)
    {
        _logger = logger;
        _databaseManager = databaseManager;
        _mapper = mapper;
    }

    public async Task<string> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var room = await _databaseManager.Room.GetRoomAsync(request.Id, false);

        if (room is null)
        {
            return string.Empty;
        }

        room = _mapper.Map<Models.Room>(request);

        _databaseManager.Room.UpdateRoom(room);

        await _databaseManager.SaveAsync();

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return room.Id;
    }
}