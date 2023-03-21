using Microsoft.AspNetCore.Mvc;
using MediatR;
using FluentValidation;
using FluentValidation.Results;

namespace RoomConfigMicroservice.Controllers;

public class BaseApiController : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}