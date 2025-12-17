using FluentValidation;
using BaselineAPIProjectController.Requests;

namespace BaselineAPIProjectController.Validations;

public class UpdateBudgetRequestValidator : AbstractValidator<UpdateBudgetRequest>
{
    public UpdateBudgetRequestValidator()
    {
        RuleFor(x => x.Budget).GreaterThanOrEqualTo(0);
    }
}

