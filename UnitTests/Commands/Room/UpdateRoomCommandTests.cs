using RoomConfigMicroservice.Commands.Room;

namespace UnitTests.Commands.Room;

public class UpdateRoomCommandTests
{
    private readonly UpdateRoomCommandHandler _sut;
    private readonly Mock<IDatabaseManager> _databaseManager = new Mock<IDatabaseManager>();
    private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
    private readonly Mock<ILogger<UpdateRoomCommandHandler>> _logger = new Mock<ILogger<UpdateRoomCommandHandler>>();

    public UpdateRoomCommandTests()
	{
        _sut = new UpdateRoomCommandHandler(_logger.Object, _databaseManager.Object, _mapper.Object);
	}

    [Fact]
    public async void Handle_ShouldReturnStringEmpty_WhenRoomIsNotInDatabase()
    {
        //Arrange
        var request = new UpdateRoomCommand() { Id = "" };

        _databaseManager.Setup(x => x.Room.GetRoomAsync(request.Id, false))
            .ReturnsAsync(() => null);

        //Act
        var result = await _sut.Handle(request, new CancellationToken());

        //Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public async void Handle_ShouldReturnRoomId_WhenRoomIsUpdated()
    {
        //Arrange
        var room = RoomSetup.SetupRoomClass();

        var request = new UpdateRoomCommand()
        {
            Id = room.Id,
            Name = "Updated room name",
            Description = "Updated room description",
            CurrentPrice = 200
        };

        var updatedRoom = new RoomConfigMicroservice.Models.Room()
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            CurrentPrice = request.CurrentPrice
        };

        _databaseManager.Setup(x => x.Room.GetRoomAsync(request.Id, false))
            .ReturnsAsync(room);

        _mapper.Setup(x => x.Map<RoomConfigMicroservice.Models.Room>(request))
            .Returns(updatedRoom);

        _databaseManager.Setup(x => x.Room.UpdateRoom(updatedRoom));

        _databaseManager.Setup(x => x.SaveAsync());

        //Act
        var result = await _sut.Handle(request, new CancellationToken());

        //Assert
        Assert.Equal(updatedRoom.Id, result);
    }
}