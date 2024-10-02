using Grpc.Core;
using MyTestMicroserviceApp.gRpcClientTestStubForService2;

namespace Web.gRpcServices;

public class Service2 : MyTestMicroserviceApp.gRpcClientTestStubForService2.Service2.Service2Base
{
    public override async Task<WarehouseGuid> GetNearestWarehouseGuidByPoint(Point point, ServerCallContext context)
    {
        
        
        await Task.CompletedTask;
    }
    
    public override async Task<Products> GetAllProductsInWarehouse(WarehouseGuid warehouseGuid, ServerCallContext context)
    {
        
        
        await Task.CompletedTask;
    }
}