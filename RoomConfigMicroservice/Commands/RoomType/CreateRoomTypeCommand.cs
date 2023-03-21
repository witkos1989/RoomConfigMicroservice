using MediatR;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.Models;
using System.Diagnostics;
using AutoMapper;

namespace RoomConfigMicroservice.Commands.RoomType;

public record CreateRoomTypeCommand : IRequest<string?>
{
    public string Name { get; init; }

    public string Description { get; init; }

    public bool PrivateBathroom { get; init; }
}

public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, string?>
{
    private readonly ILogger<CreateRoomTypeCommandHandler> _logger;
    private readonly IDatabaseManager _databaseManager;
    private readonly IMapper _mapper;

    public CreateRoomTypeCommandHandler(
        ILogger<CreateRoomTypeCommandHandler> logger,
        IDatabaseManager databaseManager,
        IMapper mapper)
    {
        _logger = logger;
        _databaseManager = databaseManager;
        _mapper = mapper;
    }

    public async Task<string?> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var roomType = _mapper.Map<Models.RoomType>(request);

        roomType.Id = Guid.NewGuid().ToString();

        try
        {
            await _databaseManager.RoomType.AddRoomTypeAsync(roomType);

            await _databaseManager.SaveAsync();
        }
        catch
        {
            return string.Empty;
        }

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return roomType.Id;
    }
}