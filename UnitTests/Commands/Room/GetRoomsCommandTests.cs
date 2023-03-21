using RoomConfigMicroservice.Commands.Room;

namespace UnitTests.Commands.Room;

public class GetRoomsCommandTests
{
    private readonly GetRoomsCommandHandler _sut;
    private readonly Mock<IDatabaseManager> _databaseManager = new Mock<IDatabaseManager>();
    private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
    private readonly Mock<ILogger<GetRoomsCommandHandler>> _logger = new Mock<ILogger<GetRoomsCommandHandler>>();

    public GetRoomsCommandTests()
	{
        _sut = new GetRoomsCommandHandler(_logger.Object, _databaseManager.Object, _mapper.Object);
    }

    [Fact]
    public async void Handle_ShouldReturnListOfRoomsDTO()
    {
        //Arrange
        var request = new GetRoomsCommand();

        var rooms = new List<RoomConfigMicroservice.Models.Room>();

        var room = RoomSetup.SetupRoomClass();

        rooms.Add(room);

        var roomsDTO = new List<RoomDTO>();

        var roomDTO = RoomSetup.SetupRoomDTO();

        roomsDTO.Add(roomDTO);

        _databaseManager.Setup(x => x.Room.GetAllRoomsAsync(false))
            .ReturnsAsync(rooms);

        _mapper.Setup(x => x.Map<ICollection<RoomDTO>>(rooms))
            .Returns(roomsDTO);

        //Act
        var result = await _sut.Handle(request, new CancellationToken());

        //Assert
        Assert.Equal(roomsDTO, result);
        Assert.Equal(roomsDTO.First().Id, result.First().Id);
    }
}