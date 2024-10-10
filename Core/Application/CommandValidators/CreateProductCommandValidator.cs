using Application.Commands;
using FluentValidation;

namespace Application.CommandValidators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.Address).NotEmpty();
        RuleFor(c => c.GeolocationLongitude).InclusiveBetween(-180, 180);
        RuleFor(c => c.GeolocationLatitude).InclusiveBetween(-90, 90);
    }
}