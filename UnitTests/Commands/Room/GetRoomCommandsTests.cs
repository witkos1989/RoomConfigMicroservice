using RoomConfigMicroservice.Commands.Room;

namespace UnitTests.Commands.Room;

public class GetRoomCommandsTests
{
	private readonly GetRoomCommandHandler _sut;
	private readonly Mock<IDatabaseManager> _databaseManager = new Mock<IDatabaseManager>();
	private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
	private readonly Mock<ILogger<GetRoomCommandHandler>> _logger = new Mock<ILogger<GetRoomCommandHandler>>();

	public GetRoomCommandsTests()
	{
		_sut = new GetRoomCommandHandler(_logger.Object, _databaseManager.Object, _mapper.Object);
	}

	[Fact]
	public async void Handle_ShouldReturnNull_WhenRoomIsNotInDatabase()
	{
		//Arrange
		var request = new GetRoomCommand() { Id = "test-id" };

		_databaseManager.Setup(x => x.Room.GetRoomAsync(request.Id, false))
			.ReturnsAsync(() => null);

		//Act
		var result = await _sut.Handle(request, new CancellationToken());

		//Assert
		Assert.Null(result);
	}

    [Fact]
    public async void Handle_ShouldReturnRoomDTO_WhenRoomIsInDatabase()
    {
        //Arrange
        var request = new GetRoomCommand() { Id = "test-id" };

		var room = RoomSetup.SetupRoomClass();

		var roomDTO = RoomSetup.SetupRoomDTO();

        _databaseManager.Setup(x => x.Room.GetRoomAsync(request.Id, false))
			.ReturnsAsync(room);

        _mapper.Setup(x => x.Map<RoomDTO>(room))
			.Returns(roomDTO);

        //Act
        var result = await _sut.Handle(request, new CancellationToken());

		//Assert
		Assert.NotNull(result);
		Assert.Equal(roomDTO.Name, result.Name);
		Assert.Equal(roomDTO.Description, result.Description);
        Assert.Equal(roomDTO.CurrentPrice, result.CurrentPrice);
    }
}