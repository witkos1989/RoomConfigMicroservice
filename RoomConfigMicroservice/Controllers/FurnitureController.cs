using Microsoft.AspNetCore.Mvc;
using RoomConfigMicroservice.Models;
using MediatR;
using RoomConfigMicroservice.Commands.Furniture;

namespace RoomConfigMicroservice.Controllers;

[ApiController]
[Route("furniture")]
public class FurnitureController : BaseApiController
{
    [HttpGet("getfurnitures")]
    public async Task<IActionResult> GetAll()
    {
        var response = await Mediator.Send(new GetFurnituresCommand());

        if (!response.Any())
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("getfurniture/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var response = await Mediator.Send(new GetFurnitureCommand() { Id = id });

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost("addfurniture")]
    public async Task<IActionResult> Add([FromBody] CreateFurnitureCommand furniture)
    {
        var response = await Mediator.Send(furniture);

        if (string.IsNullOrEmpty(response))
        {
            return BadRequest("The object was not created");
        }

        return Ok(response);
    }

    [HttpPut("updatefurniture")]
    public async Task<IActionResult> Update([FromBody] UpdateFurnitureCommand furniture)
    {
        var response = await Mediator.Send(furniture);

        if (string.IsNullOrEmpty(response))
        {
            return BadRequest("The object was not updated");
        }

        return Ok(response);
    }

    [HttpDelete("deletefurniture/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await Mediator.Send(new DeleteFurnitureCommand() { Id = id });

        if (string.IsNullOrEmpty(response))
        {
            return NotFound();
        }

        return Ok(response);
    }
}