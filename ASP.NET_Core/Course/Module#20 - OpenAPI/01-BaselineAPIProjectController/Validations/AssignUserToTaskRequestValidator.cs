using FluentValidation;
using BaselineAPIProjectController.Requests;

namespace BaselineAPIProjectController.Validations;

public class AssignUserToTaskRequestValidator : AbstractValidator<AssignUserToTaskRequest>
{
    public AssignUserToTaskRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}

