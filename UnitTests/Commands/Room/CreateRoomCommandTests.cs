using RoomConfigMicroservice.Commands.Room;

namespace UnitTests.Commands.Room;

public class CreateRoomCommandTests
{
    private readonly CreateRoomCommandHandler _sut;
    private readonly Mock<IDatabaseManager> _databaseManager = new Mock<IDatabaseManager>();
    private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
    private readonly Mock<ILogger<CreateRoomCommandHandler>> _logger = new Mock<ILogger<CreateRoomCommandHandler>>();

    public CreateRoomCommandTests()
	{
        _sut = new CreateRoomCommandHandler(_logger.Object, _databaseManager.Object, _mapper.Object);
    }

    [Fact]
    public async void Handle_ShouldReturnRoomId_WhenRoomIsCreated()
    {
        //Arrange
        var room = RoomSetup.SetupRoomClass();

        var request = new CreateRoomCommand()
        {
            Name = room.Name,
            Description = room.Description,
            CurrentPrice = room.CurrentPrice
        };

        _mapper.Setup(x => x.Map<RoomConfigMicroservice.Models.Room>(request))
            .Returns(room);

        _databaseManager.Setup(x => x.Room.AddRoomAsync(room));

        _databaseManager.Setup(x => x.SaveAsync());

        //Act
        var result = await _sut.Handle(request, new CancellationToken());

        //Assert
        Assert.Equal(room.Id, result);
    }

    [Fact]
    public async void Handle_ShouldReturnEmptyString_WhenDatabaseThrowsException()
    {
        //Arrange
        var room = RoomSetup.SetupRoomClass();

        var request = new CreateRoomCommand()
        {
            Name = room.Name,
            Description = room.Description,
            CurrentPrice = room.CurrentPrice
        };

        _mapper.Setup(x => x.Map<RoomConfigMicroservice.Models.Room>(request))
            .Returns(room);

        _databaseManager.Setup(x => x.Room.AddRoomAsync(room))
            .Throws<Exception>();

        //Act
        var result = await _sut.Handle(request, new CancellationToken());

        //Assert
        Assert.Equal(string.Empty, result);
    }

}