using Domain.Entities;
using FluentValidation;

namespace Domain.Validators;

// TODO: мб все-таки лучше валидировать только команды в Application Layer?
public class SupplyValidator : AbstractValidator<Supply>
{
    public SupplyValidator()
    {
        RuleFor(s => s).MustAsync(async (supply, _) => (await supply.GetProducts()).Count != 0).WithMessage("The supply must contain at least one product.");
        
        RuleFor(s => s).MustAsync(async (supply, _) =>
        {
            var products = await supply.GetProducts();
            var productWarehouseGuid = (await products.First().Warehouse.Value).Guid;
            foreach (var product in products)
                if ((await product.Warehouse.Value).Guid != productWarehouseGuid)
                    return false;
            return true;
        }).WithMessage("The products supplied must be from the same warehouse.");
        
        RuleFor(s => s).MustAsync(async (supply, _) =>
        {
            var products = await supply.GetProducts();
            var productsWithoutDuplicates = new HashSet<Product>(products);
            return products.Count == productsWithoutDuplicates.Count;
        }).WithMessage("The products supplied must be unique.");
    }
}