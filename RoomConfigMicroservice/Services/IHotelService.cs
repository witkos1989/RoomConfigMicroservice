using RoomConfigMicroservice.Models;

namespace RoomConfigMicroservice.Services;

public interface IHotelService
{
    Task<IEnumerable<Hotel>> GetAllHotelsAsync(bool trackChanges);

    Task<Hotel?> GetHotelAsync(string id, bool trackChanges);

    Task AddHotelAsync(Hotel hotel);

    void AddHotel(Hotel hotel);

    void RemoveHotel(Hotel hotel);

    void UpdateHotel(Hotel hotel);
}

