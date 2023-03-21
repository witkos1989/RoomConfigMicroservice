using Microsoft.EntityFrameworkCore;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.Persistence;

namespace RoomConfigMicroservice.Services;

public class RoomTypeService : DatabaseService<RoomType>, IRoomTypeService
{
    public RoomTypeService(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<IEnumerable<RoomType>> GetAllRoomTypesAsync(bool trackChanges) =>
        await FindAll(trackChanges)
        .Include(rt => rt.Furnitures)
        .OrderBy(rt => rt.Name).ToListAsync();

    public async Task<RoomType?> GetRoomTypeAsync(string id, bool trackChanges) =>
        await FindByCondition(rt => rt.Id.Equals(id), trackChanges)
        .Include(rt => rt.Furnitures)
        .SingleOrDefaultAsync();

    public void AddRoomType(RoomType roomType) =>
        Create(roomType);

    public async Task AddRoomTypeAsync(RoomType roomType) =>
        await CreateAsync(roomType);

    public void RemoveRoomType(RoomType roomType) =>
        Update(roomType);

    public void UpdateRoomType(RoomType roomType) =>
        Delete(roomType);
}