using Microsoft.EntityFrameworkCore;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.Persistence;

namespace RoomConfigMicroservice.Services;

public class RoomService : DatabaseService<Room>, IRoomService
{
	public RoomService(ApplicationDbContext db) : base(db)
	{
	}

    public async Task<IEnumerable<Room>> GetAllRoomsAsync(bool trackChanges) =>
        await FindAll(trackChanges).OrderBy(f => f.Name)
        .Include(f => f.RoomType)
        .Include(f => f.Hotel)
        .ToListAsync();

    public async Task<Room?> GetRoomAsync(string id, bool trackChanges) =>
        await FindByCondition(f => f.Id.Equals(id), trackChanges)
        .Include(f => f.RoomType)
        .Include(f => f.Hotel)
        .SingleOrDefaultAsync();

    public async Task AddRoomAsync(Room room) =>
        await CreateAsync(room);

    public void AddRoom(Room room) =>
         Create(room);

    public void RemoveRoom(Room room) =>
        Delete(room);

    public void UpdateRoom(Room room) =>
        Update(room);
}