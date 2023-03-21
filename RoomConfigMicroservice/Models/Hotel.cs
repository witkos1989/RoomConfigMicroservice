namespace RoomConfigMicroservice.Models;

public class Hotel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<Room> Rooms { get; set; }
}