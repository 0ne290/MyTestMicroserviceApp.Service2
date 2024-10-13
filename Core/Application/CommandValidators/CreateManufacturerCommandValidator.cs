using Application.Commands;
using Domain.Interfaces;
using FluentValidation;

namespace Application.CommandValidators;

public class CreateManufacturerCommandValidator : AbstractValidator<CreateManufacturerCommand>
{
    public CreateManufacturerCommandValidator(IManufacturerStorage manufacturerStorage)
    {
        RuleFor(c => c.Address).NotEmpty().WithMessage("Name must not be empty.");
        RuleFor(c => c.Name).NotEmpty().WithMessage("Address must not be empty.");
        RuleFor(c => c.Name).MustAsync(async (name, _) => !await manufacturerStorage.ExistsByName(name)).WithMessage("Manufacturer with the specified name already exists.");
        RuleFor(c => c.Address).MustAsync(async (address, _) => !await manufacturerStorage.ExistsByAddress(address)).WithMessage("Manufacturer with the specified address already exists.");
    }
}