using Microsoft.AspNetCore.Mvc;
using RoomConfigMicroservice.Commands.Room;

namespace RoomConfigMicroservice.Controllers;

[ApiController]
[Route("room")]
public class RoomController : BaseApiController
{
    [HttpGet("getrooms")]
    public async Task<IActionResult> GetAll()
    {
        var response = await Mediator.Send(new GetRoomsCommand());

        if (!response.Any())
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("getroom/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var response = await Mediator.Send(new GetRoomCommand() { Id = id });

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost("addroom")]
    public async Task<IActionResult> Add([FromBody] CreateRoomCommand room)
    {
        var response = await Mediator.Send(room);

        if (string.IsNullOrEmpty(response))
        {
            return BadRequest("The object was not created");
        }

        return Ok(response);
    }

    [HttpPut("updateroom")]
    public async Task<IActionResult> Update([FromBody] UpdateRoomCommand room)
    {
        var response = await Mediator.Send(room);

        if (string.IsNullOrEmpty(response))
        {
            return BadRequest("The object was not updated");
        }

        return Ok(response);
    }

    [HttpDelete("deleteroom")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await Mediator.Send(new DeleteRoomCommand { Id = id });

        if (string.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost("setroomtype")]
    public async Task<IActionResult> SetRoomType([FromBody] SetRoomTypeCommand set)
    {
        var response = await Mediator.Send(set);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}