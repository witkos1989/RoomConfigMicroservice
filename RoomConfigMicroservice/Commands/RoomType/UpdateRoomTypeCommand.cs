using MediatR;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.Models;
using System.Diagnostics;
using AutoMapper;

namespace RoomConfigMicroservice.Commands.RoomType;

public record UpdateRoomTypeCommand : IRequest<string>
{
    public string Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public bool PrivateBathroom { get; init; }
}

public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, string>
{
    private readonly ILogger<UpdateRoomTypeCommandHandler> _logger;
    private readonly IDatabaseManager _databaseManager;
    private readonly IMapper _mapper;

    public UpdateRoomTypeCommandHandler(
        ILogger<UpdateRoomTypeCommandHandler> logger,
        IDatabaseManager databaseManager,
        IMapper mapper)
    {
        _logger = logger;
        _databaseManager = databaseManager;
        _mapper = mapper;
    }

    public async Task<string> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var roomType = await _databaseManager.RoomType.GetRoomTypeAsync(request.Id, false);

        if (roomType is null)
        {
            return string.Empty;
        }

        roomType = _mapper.Map<Models.RoomType>(request);

        _databaseManager.RoomType.UpdateRoomType(roomType);

        await _databaseManager.SaveAsync();

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return roomType.Id;
    }
}