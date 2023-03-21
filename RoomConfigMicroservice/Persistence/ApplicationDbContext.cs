using Microsoft.EntityFrameworkCore;
using RoomConfigMicroservice.Models;

namespace RoomConfigMicroservice.Persistence;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		:base(options)
	{
	}

	public DbSet<Hotel> Hotels => Set<Hotel>();

    public DbSet<Room> Rooms => Set<Room>();

    public DbSet<RoomType> RoomTypes => Set<RoomType>();

    public DbSet<Furniture> Furnitures => Set<Furniture>();
}