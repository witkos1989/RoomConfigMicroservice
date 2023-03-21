using RoomConfigMicroservice.Models;

namespace RoomConfigMicroservice.Services;

public interface IRoomService
{
    Task<IEnumerable<Room>> GetAllRoomsAsync(bool trackChanges);

    Task<Room?> GetRoomAsync(string id, bool trackChanges);

    Task AddRoomAsync(Room room);

    void AddRoom(Room room);

    void RemoveRoom(Room room);

    void UpdateRoom(Room room);
}