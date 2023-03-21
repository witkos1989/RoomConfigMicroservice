namespace RoomConfigMicroservice.DTOs;

public class HotelDTO
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<RoomDTO> Rooms { get; set; }
}