using Application.Commands;
using FluentValidation;

namespace Application.CommandValidators;

public class CreateManufacturerCommandValidator : AbstractValidator<CreateManufacturerCommand>
{
    public CreateManufacturerCommandValidator()
    {
        RuleFor(c => c.Address).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}