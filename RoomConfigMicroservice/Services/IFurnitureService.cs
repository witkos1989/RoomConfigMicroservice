using RoomConfigMicroservice.Models;

namespace RoomConfigMicroservice.Services;

public interface IFurnitureService
{
    Task<IEnumerable<Furniture>> GetAllFurnituresAsync(bool trackChanges);

    Task<Furniture?> GetFurnitureAsync(string id, bool trackChanges);

    Task AddFurnitureAsync(Furniture furniture);

    void AddFurniture(Furniture furniture);

    void RemoveFurniture(Furniture furniture);

    void UpdateFurniture(Furniture furniture);
}