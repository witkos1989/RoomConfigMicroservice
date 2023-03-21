namespace RoomConfigMicroservice.DTOs;

public class RoomDTO
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal CurrentPrice { get; set; }

    public RoomTypeDTO? RoomType { get; set; }
}