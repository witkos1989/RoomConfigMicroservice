using MediatR;
using AutoMapper;
using RoomConfigMicroservice.Services;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.DTOs;
using System.Diagnostics;

namespace RoomConfigMicroservice.Commands.RoomType;

public record GetRoomTypesCommand : IRequest<IEnumerable<RoomTypeDTO>>
{ }

public class GetRoomTypesCommandHandler : IRequestHandler<GetRoomTypesCommand, IEnumerable<RoomTypeDTO>>
{
    private readonly ILogger<GetRoomTypesCommandHandler> _logger;
    private readonly IDatabaseManager _databaseManager;
    private readonly IMapper _mapper;

    public GetRoomTypesCommandHandler(
        ILogger<GetRoomTypesCommandHandler> logger,
        IDatabaseManager databaseManager,
        IMapper mapper)
    {
        _logger = logger;
        _databaseManager = databaseManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoomTypeDTO>> Handle(GetRoomTypesCommand request, CancellationToken cancellationToken)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var roomTypes = await _databaseManager.RoomType.GetAllRoomTypesAsync(false);

        var roomTypesDTO = _mapper.Map<ICollection<RoomTypeDTO>>(roomTypes);

        stopwatch.Stop();
        _logger.Log(LogLevel.Information, "Time of operation {1} ms", stopwatch.ElapsedMilliseconds);

        return roomTypesDTO;
    }
}