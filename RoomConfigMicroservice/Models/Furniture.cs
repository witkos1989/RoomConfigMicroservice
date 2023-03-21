namespace RoomConfigMicroservice.Models;

public class Furniture
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<RoomType> RoomTypes { get; set; }
}