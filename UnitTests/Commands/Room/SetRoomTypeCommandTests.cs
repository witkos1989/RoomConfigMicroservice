using RoomConfigMicroservice.Commands.Room;

namespace UnitTests.Commands.Room;

public class SetRoomTypeCommandTests
{
    private readonly SetRoomTypeCommandHandler _sut;
    private readonly Mock<IDatabaseManager> _databaseManager = new Mock<IDatabaseManager>();
    private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
    private readonly Mock<ILogger<SetRoomTypeCommandHandler>> _logger = new Mock<ILogger<SetRoomTypeCommandHandler>>();

    public SetRoomTypeCommandTests()
	{
        _sut = new SetRoomTypeCommandHandler(_logger.Object, _databaseManager.Object, _mapper.Object);
	}

    [Fact]
    public async void Handle_ShouldReturnNull_WhenRoomIsNotInDatabase()
    {
        //Arrange
        var roomType = RoomSetup.SetupRoomTypeClass();

        var request = new SetRoomTypeCommand()
        {
            RoomId = "",
            RoomTypeId = roomType.Id
        };

        _databaseManager.Setup(x => x.Room.GetRoomAsync(request.RoomId, true))
            .ReturnsAsync(() => null);

        _databaseManager.Setup(x => x.RoomType.GetRoomTypeAsync(request.RoomTypeId, true))
            .ReturnsAsync(roomType);

        //Act
        var result = await _sut.Handle(request, new CancellationToken());

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async void Handle_ShouldReturnNull_WhenRoomTypeIsNotInDatabase()
    {
        //Arrange
        var room = RoomSetup.SetupRoomClass();

        var request = new SetRoomTypeCommand()
        {
            RoomId = room.Id,
            RoomTypeId = ""
        };

        _databaseManager.Setup(x => x.Room.GetRoomAsync(request.RoomId, true))
            .ReturnsAsync(room);

        _databaseManager.Setup(x => x.RoomType.GetRoomTypeAsync(request.RoomTypeId, true))
            .ReturnsAsync(() => null);

        //Act
        var result = await _sut.Handle(request, new CancellationToken());

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async void Handle_ShouldReturnRoomDTO_WhenRoomTypeIsAssignedToRoom()
    {
        //Arrange
        var roomType = RoomSetup.SetupRoomTypeClass();

        var room = RoomSetup.SetupRoomClass();

        var roomDTO = RoomSetup.SetupRoomDTO();

        roomDTO.RoomType = RoomSetup.SetupRoomTypeDTO();

        var request = new SetRoomTypeCommand()
        {
            RoomId = room.Id,
            RoomTypeId = roomType.Id
        };

        _databaseManager.Setup(x => x.Room.GetRoomAsync(request.RoomId, true))
            .ReturnsAsync(room);

        _databaseManager.Setup(x => x.RoomType.GetRoomTypeAsync(request.RoomTypeId, true))
            .ReturnsAsync(roomType);

        _databaseManager.Setup(x => x.SaveAsync());

        _mapper.Setup(x => x.Map<RoomDTO>(room))
            .Returns(roomDTO);

        //Act
        var result = await _sut.Handle(request, new CancellationToken());

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.RoomType);
        Assert.Equal(roomDTO.Id, result.Id);
        Assert.Equal(roomDTO.RoomType.Id, result.RoomType.Id);
    }
}