using RoomConfigMicroservice.Models;

namespace RoomConfigMicroservice.DTOs;

public class RoomTypeDTO
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool PrivateBathroom { get; set; }

    public ICollection<FurnitureDTO> Furnitures { get; set; }
}