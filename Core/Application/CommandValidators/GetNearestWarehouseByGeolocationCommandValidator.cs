using Application.Commands;
using FluentValidation;

namespace Application.CommandValidators;

public class GetNearestWarehouseByGeolocationCommandValidator : AbstractValidator<GetNearestWarehouseByGeolocationCommand>
{
    public GetNearestWarehouseByGeolocationCommandValidator()
    {
        RuleFor(c => c.GeolocationLongitude).InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be in range [-180; 180].");
        RuleFor(c => c.GeolocationLatitude).InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be in range [-90; 90].");
    }
}