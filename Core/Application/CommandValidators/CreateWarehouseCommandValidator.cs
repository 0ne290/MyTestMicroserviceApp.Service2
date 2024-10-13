using Application.Commands;
using Domain.Interfaces;
using FluentValidation;

namespace Application.CommandValidators;

public class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
{
    public CreateWarehouseCommandValidator(IWarehouseStorage warehouseStorage)
    {
        RuleFor(c => c.Address).NotEmpty().WithMessage("Address must not be empty.");
        RuleFor(c => c.GeolocationLongitude).InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be in range [-180; 180].");
        RuleFor(c => c.GeolocationLatitude).InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be in range [-90; 90].");
        RuleFor(c => c.Address).MustAsync(async (address, _) => !await warehouseStorage.ExistsByAddress(address))
            .WithMessage("Warehouse with the specified address already exists.");
        RuleFor(c => c)
            .MustAsync(async (request, _) =>
                !await warehouseStorage.ExistsByGeolocation((request.GeolocationLongitude,
                    request.GeolocationLatitude)))
            .WithMessage("Warehouse with the specified geolocation already exists.");
    }
}