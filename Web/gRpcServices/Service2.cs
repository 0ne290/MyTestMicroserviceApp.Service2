using Grpc.Core;
using MyTestMicroserviceApp.gRpcClientTestStubForService2;

namespace Web.gRpcServices;

public class Service2 : MyTestMicroserviceApp.gRpcClientTestStubForService2.Service2.Service2Base
{
    public override async Task<WarehouseGuid> GetNearestWarehouseGuidByPoint(Point point, ServerCallContext context)
    {
        throw new NotImplementedException();
        
        await Task.CompletedTask;
    }
    
    public override async Task<Products> GetAllProductsInWarehouse(WarehouseGuid warehouseGuid, ServerCallContext context)
    {
        throw new NotImplementedException();
        
        await Task.CompletedTask;
    }
}