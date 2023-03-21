namespace RoomConfigMicroservice.Models;

public class Room
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal CurrentPrice { get; set; }

    public RoomType? RoomType { get; set; }

    public Hotel? Hotel { get; set; }
}