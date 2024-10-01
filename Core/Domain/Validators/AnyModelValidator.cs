using Domain.Constants;
using Domain.Models;
using FluentValidation;

namespace Domain.Validators;

public class AnyModelValidator : AbstractValidator<AnyModel>
{
    public AnyModelValidator()
    {
        RuleFor(m => m.AnyItem).NotEmpty();
        When(m => m.Status == StatusesOfAnyModel.AwaitingCallOfMethod2,
            () => { RuleFor(m => m.AnyRelatedItem1).NotEmpty(); });
        When(m => m.Status == StatusesOfAnyModel.AllMethodsAreCalled && m.AnyRelatedItem2 != null,
            () =>
            {
                RuleFor(m => m.AnyRelatedItem3).NotEmpty();
                RuleFor(m => m.AnyRelatedItem4).NotEmpty();
            });
        When(m => m.Status == StatusesOfAnyModel.AllMethodsAreCalled && m.AnyRelatedItem2 == null,
            () =>
            {
                RuleFor(m => m.AnyRelatedItem3).NotEmpty();
                RuleFor(m => m.AnyRelatedItem4).Null();
            });
    }
}