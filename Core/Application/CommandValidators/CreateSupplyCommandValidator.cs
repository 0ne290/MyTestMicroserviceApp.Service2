using Application.Commands;
using Domain.Interfaces;
using FluentValidation;

namespace Application.CommandValidators;

public class CreateSupplyCommandValidator : AbstractValidator<CreateSupplyCommand>
{
    public CreateSupplyCommandValidator(IProductStorage productStorage)
    {
        RuleFor(c => c.ProductGuids)
            .CustomAsync(async (productGuids, context, _) =>
            {
                if (productGuids.Count == 0)
                {
                    context.AddFailure("The supply must contain at least one product.");
                    return;
                }
                
                var products = (await productStorage.GetAllByGuids(productGuids)).ToList();
                
                if (products.Count != productGuids.Count)
                    context.AddFailure("Product with one of the specified guids does not exist.");
                else if (products.Any(p => p.Supply != null))
                    context.AddFailure("Product with one of the specified guids has already been supplied.");
                else
                {
                    var productWarehouseGuid = (await products.First().Warehouse.Value).Guid;
                    foreach (var product in products)
                    {
                        if ((await product.Warehouse.Value).Guid == productWarehouseGuid)
                            continue;
                        
                        context.AddFailure("The products supplied must be from the same warehouse.");
                        return;
                    }
                }
            });
    }
}