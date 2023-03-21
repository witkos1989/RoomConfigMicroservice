using RoomConfigMicroservice.Models;

namespace RoomConfigMicroservice.Services;

public interface IRoomTypeService
{
    Task<IEnumerable<RoomType>> GetAllRoomTypesAsync(bool trackChanges);

    Task<RoomType?> GetRoomTypeAsync(string id, bool trackChanges);

    Task AddRoomTypeAsync(RoomType roomType);

    void AddRoomType(RoomType roomType);

    void RemoveRoomType(RoomType roomType);

    void UpdateRoomType(RoomType roomType);
}