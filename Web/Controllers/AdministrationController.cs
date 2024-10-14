using Application.Commands;
using Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Web.Controllers;

[Route("administration")]
public class AdministrationController : Controller
{
    public AdministrationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Route("get-all-manufacturers")]
    [HttpGet]
    public async Task<IActionResult> GetAllManufacturers() => Ok(JsonConvert.SerializeObject(await _mediator.Send(new GetAllManufacturersCommand()), Formatting.Indented));
    
    [Route("get-all-warehouses")]
    [HttpGet]
    public async Task<IActionResult> GetAllWarehouses() => Ok(JsonConvert.SerializeObject(await _mediator.Send(new GetAllWarehousesCommand()), Formatting.Indented));
    
    [Route("get-all-products")]
    [HttpGet]
    public async Task<IActionResult> GetAllProducts() => Ok(JsonConvert.SerializeObject(await _mediator.Send(new GetAllProductsCommand()), Formatting.Indented));
    
    [Route("get-all-supplies")]
    [HttpGet]
    public async Task<IActionResult> GetAllSupplies() => Ok(JsonConvert.SerializeObject(await _mediator.Send(new GetAllSuppliesCommand()), Formatting.Indented));

    [Route("create-manufacturer")]
    [HttpPost]
    public async Task<IActionResult> CreateManufacturer([FromBody] CreateManufacturerCommand request)
    {
        var result = await _mediator.Send(request);

        if (result.IsFailed)
            return BadRequest(result.ErrorsToJson());

        return Ok();
    }
    
    [Route("create-warehouse")]
    [HttpPost]
    public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseCommand request)
    {
        var result = await _mediator.Send(request);

        if (result.IsFailed)
            return BadRequest(result.ErrorsToJson());

        return Ok();
    }
    
    [Route("create-product")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
    {
        var result = await _mediator.Send(request);

        if (result.IsFailed)
            return BadRequest(result.ErrorsToJson());

        return Ok();
    }
    
    [Route("create-supply")]
    [HttpPost]
    public async Task<IActionResult> CreateSupply([FromBody] CreateSupplyCommand request)
    {
        var result = await _mediator.Send(request);

        if (result.IsFailed)
            return BadRequest(result.ErrorsToJson());

        return Ok();
    }
    
    private readonly IMediator _mediator;
}