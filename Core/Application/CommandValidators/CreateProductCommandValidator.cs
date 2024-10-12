using Application.Commands;
using Domain.Interfaces;
using FluentValidation;

namespace Application.CommandValidators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator(IManufacturerStorage manufacturerStorage, IWarehouseStorage warehouseStorage)
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name must not be empty.");
        RuleFor(c => c.ManufacturerGuid)
            .MustAsync(async (manufacturerGuid, _) => await manufacturerStorage.ExistsByGuid(manufacturerGuid))
            .WithMessage("Manufacturer with the specified guid does not exist.");
        RuleFor(c => c.WarehouseGuid)
            .MustAsync(async (warehouseGuid, _) => await warehouseStorage.ExistsByGuid(warehouseGuid))
            .WithMessage("Warehouse with the specified guid does not exist.");
    }
}