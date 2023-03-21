using Microsoft.EntityFrameworkCore;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.Persistence;

namespace RoomConfigMicroservice.Services;

	public class FurnitureService : DatabaseService<Furniture>, IFurnitureService
	{
		public FurnitureService(ApplicationDbContext db) : base(db)
		{
		}

    public async Task<IEnumerable<Furniture>> GetAllFurnituresAsync(bool trackChanges) =>
        await FindAll(trackChanges).OrderBy(f => f.Name).ToListAsync();

    public async Task<Furniture?> GetFurnitureAsync(string id, bool trackChanges) =>
        await FindByCondition(f => f.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

    public async Task AddFurnitureAsync(Furniture furniture) =>
        await CreateAsync(furniture);

    public void AddFurniture(Furniture furniture) =>
        Create(furniture);

    public void RemoveFurniture(Furniture furniture) =>
        Delete(furniture);

    public void UpdateFurniture(Furniture furniture) =>
        Update(furniture);
}