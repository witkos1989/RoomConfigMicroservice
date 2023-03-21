using Microsoft.EntityFrameworkCore;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.Persistence;

namespace RoomConfigMicroservice.Services;

public class HotelService : DatabaseService<Hotel>, IHotelService
{
	public HotelService(ApplicationDbContext db) : base(db)
    {
	}

    public async Task<IEnumerable<Hotel>> GetAllHotelsAsync(bool trackChanges) =>
        await FindAll(trackChanges).OrderBy(f => f.Name)
        .Include(f => f.Rooms)
        .ToListAsync();

    public async Task<Hotel?> GetHotelAsync(string id, bool trackChanges) =>
        await FindByCondition(f => f.Id.Equals(id), trackChanges)
        .Include(f => f.Rooms)
        .SingleOrDefaultAsync();

    public async Task AddHotelAsync(Hotel hotel) =>
        await CreateAsync(hotel);

    public void AddHotel(Hotel hotel) =>
         Create(hotel);

    public void RemoveHotel(Hotel hotel) =>
        Delete(hotel);

    public void UpdateHotel(Hotel hotel) =>
        Update(hotel);
}