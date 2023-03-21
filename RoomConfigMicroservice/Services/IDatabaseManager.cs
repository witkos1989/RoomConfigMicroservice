namespace RoomConfigMicroservice.Services;

public interface IDatabaseManager
{
    IFurnitureService Furniture { get; }

    IRoomTypeService RoomType { get; }

    IRoomService Room { get; }

    IHotelService Hotel { get; }

    Task SaveAsync();
}