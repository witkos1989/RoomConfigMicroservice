using RoomConfigMicroservice.Persistence;

namespace RoomConfigMicroservice.Services;

public class DatabaseManager : IDatabaseManager
{
    private readonly ApplicationDbContext _db;
    private IFurnitureService _furnitureService;
    private IRoomTypeService _roomTypeService;
    private IRoomService _roomService;
    private IHotelService _hotelService;

    public DatabaseManager(ApplicationDbContext db) => _db = db;

    public IFurnitureService Furniture
    {
        get
        {
            if (_furnitureService == null)
            {
                _furnitureService = new FurnitureService(_db);
            }
            return _furnitureService;
        }
    }

    public IRoomTypeService RoomType
    {
        get
        {
            if (_roomTypeService == null)
            {
                _roomTypeService = new RoomTypeService(_db);
            }
            return _roomTypeService;
        }
    }

    public IRoomService Room
    {
        get
        {
            if (_roomService == null)
            {
                _roomService = new RoomService(_db);
            }
            return _roomService;
        }
    }

    public IHotelService Hotel
    {
        get
        {
            if (_hotelService == null)
            {
                _hotelService = new HotelService(_db);
            }
            return _hotelService;
        }
    }

    public async Task SaveAsync() => await _db.SaveChangesAsync();
}