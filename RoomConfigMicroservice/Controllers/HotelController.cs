using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoomConfigMicroservice.Models;
using RoomConfigMicroservice.Commands.Hotel;

namespace RoomConfigMicroservice.Controllers;

[ApiController]
[Route("hotel")]
public class HotelController : BaseApiController
{
    [HttpGet("gethotels")]
    public async Task<IActionResult> GetAll()
    {
        var response = await Mediator.Send(new GetHotelsCommand());

        if (!response.Any())
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("gethotel/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var response = await Mediator.Send(new GetHotelCommand() { Id = id });

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost("addhotel")]
    public async Task<IActionResult> Add([FromBody] CreateHotelCommand furniture)
    {
        var response = await Mediator.Send(furniture);

        if (string.IsNullOrEmpty(response))
        {
            return BadRequest("The object was not created");
        }

        return Ok(response);
    }

    [HttpPut("updatehotel")]
    public async Task<IActionResult> Update([FromBody] UpdateHotelCommand furniture)
    {
        var response = await Mediator.Send(furniture);

        if (string.IsNullOrEmpty(response))
        {
            return BadRequest("The object was not updated");
        }

        return Ok(response);
    }

    [HttpDelete("deletehotel/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await Mediator.Send(new DeleteHotelCommand() { Id = id });

        if (string.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost("assignroom")]
    public async Task<IActionResult> AssignRoom([FromBody] AssignRoomCommand assign)
    {
        var response = await Mediator.Send(assign);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}