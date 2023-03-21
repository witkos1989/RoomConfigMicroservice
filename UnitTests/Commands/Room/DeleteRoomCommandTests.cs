using RoomConfigMicroservice.Commands.Room;

namespace UnitTests.Commands.Room;

public class DeleteRoomCommandTests
{
    private readonly DeleteRoomCommandHandler _sut;
    private readonly Mock<IDatabaseManager> _databaseManager = new Mock<IDatabaseManager>();
    private readonly Mock<ILogger<DeleteRoomCommandHandler>> _logger = new Mock<ILogger<DeleteRoomCommandHandler>>();


    public DeleteRoomCommandTests()
	{
        _sut = new DeleteRoomCommandHandler(_logger.Object, _databaseManager.Object);
    }

    [Fact]
    public async void Handle_ShouldReturnEmptyString_WhenRoomIsNotInDatabase()
    {
        //Arrange
        var request = new DeleteRoomCommand() { Id = "" };

        _databaseManager.Setup(x => x.Room.GetRoomAsync(request.Id, false))
            .ReturnsAsync(() => null);

        //Act
        var result = await _sut.Handle(request, new CancellationToken());

        //Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public async void Handle_ShouldReturnDeletedString_WhenRoomIsDeleted()
    {
        //Arrange
        var room = RoomSetup.SetupRoomClass();

        var request = new DeleteRoomCommand() { Id = room.Id };

        _databaseManager.Setup(x => x.Room.GetRoomAsync(request.Id, false))
            .ReturnsAsync(room);

        _databaseManager.Setup(x => x.Room.RemoveRoom(room));

        _databaseManager.Setup(x => x.SaveAsync());

        //Act
        var result = await _sut.Handle(request, new CancellationToken());

        //Assert
        Assert.Equal("Room deleted", result);
    }
}