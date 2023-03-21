namespace UnitTests.Commands.Room;

public static class RoomSetup
{
	public static RoomConfigMicroservice.Models.Room SetupRoomClass()
	{
        var room = new RoomConfigMicroservice.Models.Room
        {
            Id = "abc-123-def-456",
            Name = "Test room name",
            Description = "Test room description",
            CurrentPrice = 100
        };

        return room;
    }

    public static RoomDTO SetupRoomDTO()
    {
        var roomDTO = new RoomDTO
        {
            Id = "abc-123-def-456",
            Name = "Test room name",
            Description = "Test room description",
            CurrentPrice = 100
        };

        return roomDTO;
    }

    public static RoomType SetupRoomTypeClass()
    {
        var roomType = new RoomType
        {
            Id = "fgh-678-ijk-901",
            Name = "Test room type name",
            Description = "Test room type description",
            PrivateBathroom = true
        };

        return roomType;
    }

    public static RoomTypeDTO SetupRoomTypeDTO()
    {
        var roomTypeDTO = new RoomTypeDTO
        {
            Id = "fgh-678-ijk-901",
            Name = "Test room type name",
            Description = "Test room type description",
            PrivateBathroom = true
        };

        return roomTypeDTO;
    }
}