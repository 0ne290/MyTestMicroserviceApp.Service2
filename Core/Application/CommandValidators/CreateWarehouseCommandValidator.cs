using Application.Commands;
using FluentValidation;

namespace Application.CommandValidators;

public class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
{
    public CreateWarehouseCommandValidator()
    {
        RuleFor(c => c.Address).NotEmpty();
        RuleFor(c => c.GeolocationLongitude).InclusiveBetween(-180, 180);
        RuleFor(c => c.GeolocationLatitude).InclusiveBetween(-90, 90);
    }
}