using Microsoft.AspNetCore.Mvc;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.Commands.RoomType;
using MediatR;

namespace RoomConfigMicroservice.Controllers;

[ApiController]
[Route("roomtype")]
public class RoomTypeController : BaseApiController
{
    [HttpGet("getroomtypes")]
    public async Task<IActionResult> GetAll()
    {
        var response = await Mediator.Send(new GetRoomTypesCommand());

        if (!response.Any())
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("getroomtype/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var response = await Mediator.Send(new GetRoomTypeCommand() { Id = id });

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost("addroomtype")]
    public async Task<IActionResult> Add([FromBody] CreateRoomTypeCommand roomType)
    {
        var response = await Mediator.Send(roomType);

        if (string.IsNullOrEmpty(response))
        {
            return BadRequest("The object was not created");
        }

        return Ok(response);
    }

    [HttpPut("updateroomtype")]
    public async Task<IActionResult> Update([FromBody] UpdateRoomTypeCommand roomType)
    {
        var response = await Mediator.Send(roomType);

        if (string.IsNullOrEmpty(response))
        {
            return BadRequest("The object was not updated");
        }

        return Ok(response);
    }

    [HttpDelete("deleteroomtype")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await Mediator.Send(new DeleteRoomTypeCommand { Id = id });

        if (string.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost("assignfurniture")]
    public async Task<IActionResult> AssignFurniture([FromBody] AssignFurnitureCommand assignFurniture)
    {
        var response = await Mediator.Send(assignFurniture);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}