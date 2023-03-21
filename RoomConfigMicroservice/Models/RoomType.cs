namespace RoomConfigMicroservice.Models;

public class RoomType
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool PrivateBathroom { get; set; }

    public ICollection<Furniture> Furnitures { get; set; }

    public ICollection<Room> Rooms { get; set; }
}