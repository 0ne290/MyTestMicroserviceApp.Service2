using Application.Commands;
using Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("administration")]
public class AdministrationController : Controller
{
    public AdministrationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("create-manufacturer")]
    [HttpPost]
    public async Task<IActionResult> CreateManufacturer([FromBody] CreateManufacturerCommand request)
    {
        var result = await _mediator.Send(request);

        if (result.IsFailed)
            return BadRequest(result.ErrorsToJson());

        return Ok();
    }
    
    private readonly IMediator _mediator;
}