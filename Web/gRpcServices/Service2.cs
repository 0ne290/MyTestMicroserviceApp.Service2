using Application.Commands;
using Application.Extensions;
using Grpc.Core;
using MediatR;
using MyTestMicroserviceApp.ServerOfgRpcService2;

namespace Web.gRpcServices;

public class Service2 : MyTestMicroserviceApp.ServerOfgRpcService2.Service2.Service2Base
{
    public Service2(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override async Task<Warehouse> GetNearestWarehouseByGeolocation(Geolocation geolocation, ServerCallContext context)
    {
        var result = await _mediator.Send(new GetNearestWarehouseByGeolocationCommand
            { GeolocationLongitude = geolocation.Longitude, GeolocationLatitude = geolocation.Latitude });
        
        if (result.IsFailed)
            throw new RpcException(new Status(StatusCode.InvalidArgument, result.ErrorsToJson()));

        return new Warehouse
        {
            Guid = result.Value.Guid, Address = result.Value.Address, GeolocationLongitude = result.Value.GeolocationLongitude,
            GeolocationLatitude = result.Value.GeolocationLatitude
        };
    }
    
    public override async Task<Products> GetAllProductsByWarehouseGuid(WarehouseGuid warehouseGuid, ServerCallContext context)
    {
        var result = await _mediator.Send(new GetAllProductsByWarehouseGuidCommand
            { WarehouseGuid = warehouseGuid.Value });
        
        if (result.IsFailed)
            throw new RpcException(new Status(StatusCode.InvalidArgument, result.ErrorsToJson()));
        
        var ret = new Products();
        foreach (var product in result.Value)
            ret.Values.Add(new Product
                { Guid = product.Guid, Name = product.Name, WarehouseGuid = product.WarehouseGuid });

        return ret;
    }

    private readonly IMediator _mediator;
}