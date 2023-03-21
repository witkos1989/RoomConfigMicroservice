using MediatR;
using AutoMapper;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.Models;
using System.Diagnostics;

namespace RoomConfigMicroservice.Commands.Room;

public record CreateRoomCommand : IRequest<string>
{
    public string Name { get; init; }

    public string Description { get; init; }

    public decimal CurrentPrice { get; init; }
}

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, string>
{
    private readonly ILogger<CreateRoomCommandHandler> _logger;
    private readonly IDatabaseManager _databaseManager;
    private readonly IMapper _mapper;


    public CreateRoomCommandHandler(
        ILogger<CreateRoomCommandHandler> logger,
        IDatabaseManager databaseManager,
        IMapper mapper)
    {
        _logger = logger;
        _databaseManager = databaseManager;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var room = _mapper.Map<Models.Room>(request);

        room.Id = Guid.NewGuid().ToString();

        try
        {
            await _databaseManager.Room.AddRoomAsync(room);

            await _databaseManager.SaveAsync();
        }
        catch
        {
            return string.Empty;
        }


        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return room.Id;
    }
}
