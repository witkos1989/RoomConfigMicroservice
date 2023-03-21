using AutoMapper;
using RoomConfigMicroservice.Commands.Furniture;
using RoomConfigMicroservice.Commands.RoomType;
using RoomConfigMicroservice.Commands.Room;
using RoomConfigMicroservice.Commands.Hotel;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.DTOs;

namespace RoomConfigMicroservice.Mapper;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<CreateFurnitureCommand, Furniture>();
        CreateMap<UpdateFurnitureCommand, Furniture>();
		CreateMap<CreateRoomTypeCommand, RoomType>();
        CreateMap<UpdateRoomTypeCommand, RoomType>();
		CreateMap<CreateRoomCommand, Room>();
		CreateMap<UpdateRoomCommand, Room>();
		CreateMap<CreateHotelCommand, Hotel>();
		CreateMap<UpdateHotelCommand, Hotel>();
		CreateMap<RoomType, RoomTypeDTO>();
		CreateMap<Furniture, FurnitureDTO>();
        CreateMap<Room, RoomDTO>();
		CreateMap<Hotel, HotelDTO>();
        CreateMap<ICollection<RoomTypeDTO>, ICollection<RoomType>>();
		CreateMap<ICollection<RoomDTO>, ICollection<Room>>();
        CreateMap<ICollection<HotelDTO>, ICollection<Hotel>>();
    }
}