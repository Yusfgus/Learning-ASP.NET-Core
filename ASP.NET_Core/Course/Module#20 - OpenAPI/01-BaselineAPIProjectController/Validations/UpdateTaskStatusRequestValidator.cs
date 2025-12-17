using FluentValidation;
using BaselineAPIProjectController.Requests;

namespace BaselineAPIProjectController.Validations;

public class UpdateTaskStatusRequestValidator : AbstractValidator<UpdateTaskStatusRequest>
{
    public UpdateTaskStatusRequestValidator()
    {
        RuleFor(x => x.Status).IsInEnum();
    }
}

