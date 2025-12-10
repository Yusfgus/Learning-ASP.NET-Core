using System.ComponentModel.DataAnnotations;

namespace ControllerDataAnnotations.Validators;

public class RequiredIfAttribute : ValidationAttribute
{
    private readonly string _dependentProperty;
    private readonly object? _targetValue;


    public RequiredIfAttribute(string dependentProperty, object? targetValue)
    {
        _dependentProperty = dependentProperty;
        _targetValue = targetValue;
    }


    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var containerType = validationContext.ObjectInstance.GetType();
        var field = containerType.GetProperty(_dependentProperty);

        if (field == null)
            return new ValidationResult($"Unknown property: {_dependentProperty}");

        var dependentValue = field.GetValue(validationContext.ObjectInstance, null);

        if (Equals(dependentValue, _targetValue))
        {
            if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} is required.");
        }
        else
        {
            if (value != null && !(value is string str && string.IsNullOrEmpty(str)))
                return new ValidationResult($"{validationContext.DisplayName} can't have value");
        }

        return ValidationResult.Success;
    }
}