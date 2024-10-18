using Application.Commands;
using Domain.Interfaces;
using FluentValidation;

namespace Application.CommandValidators;

public class GetAllProductsByWarehouseGuidCommandValidator : AbstractValidator<GetAllProductsByWarehouseGuidCommand>
{
    public GetAllProductsByWarehouseGuidCommandValidator(IWarehouseStorage warehouseStorage)
    {
        RuleFor(c => c.WarehouseGuid).NotEmpty()
            .WithMessage("Warehouse guid must not be empty.");
        RuleFor(c => c.WarehouseGuid)
            .MustAsync(async (warehouseGuid, _) => await warehouseStorage.ExistsByGuid(warehouseGuid))
            .WithMessage("Warehouse with the specified guid does not exist.");
    }
}