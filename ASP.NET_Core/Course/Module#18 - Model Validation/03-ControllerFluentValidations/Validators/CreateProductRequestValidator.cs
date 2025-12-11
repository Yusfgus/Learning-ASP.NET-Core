
using ControllerFluentValidations.Requests;
using FluentValidation;

namespace ControllerFluentValidations.Validators;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product Name is required")
            .Length(3, 255).WithMessage("Product Name must be between 3 and 255 characters");

        RuleFor(x => x.Description)
            .MinimumLength(1000).WithMessage("Description can't exceed 1000 characters");
    
        RuleFor(x => x.SKU)
            .NotEmpty().WithMessage("Product SKU is required")
            .Matches(@"^PRD-\d{5}$").WithMessage("SKU must be 'PRD-' followed by 5 digits 'PRD-XXXXX'");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Product Price is required")
            .GreaterThanOrEqualTo(0.01m).WithMessage("Price must be at least 0.01");

        RuleFor(x => x.StockQuantity)
            .NotEmpty().WithMessage("Product Stock Quantity is required")
            .GreaterThanOrEqualTo(0).WithMessage("Stock Quantity can't be negative");
    
        RuleFor(x => x.LaunchDate)
            .NotEmpty().WithMessage("Product Launch Date is required")
            .Must(BeTodayOrFuture).WithMessage("Launch date must be today or in the future");
    
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Product Category is required")
            .IsInEnum().WithMessage("Invalid product category");

        RuleFor(x =>x.ImageUrl)
            .Must(BeValidUrl).WithMessage("Image Url is not valid");

        RuleFor(x => x.Weight)
            .NotEmpty().WithMessage("Product Weight is required")
            .InclusiveBetween(0.01m, 1000m).WithMessage("Weight must be between 0.01 and 1000 kg");

        RuleFor(x => x.WarrantyPeriodMonths)
            .Must(MustBe12_24_36).WithMessage("Warranty must be 0, 12, 24, or 36 months only");

        RuleFor(x => x.IsReturnable)
            .NotEmpty().WithErrorCode("Product Returnable state is required");

        When(x => x.IsReturnable, () =>
        {
            RuleFor(x => x.ReturnPolicyDescription)
                .NotEmpty().WithMessage("Return policy description is required if the product is returnable.");
        });

        RuleFor(x => x.Tags)
            .Must(tags => tags.Count <= 5).WithMessage("A maximum of 5 tags is allowed.");
    }

    private bool BeTodayOrFuture(DateTime datetime)
        => datetime.Date >= DateTime.UtcNow.Date;

    private bool BeValidUrl(string? url)
        => Uri.TryCreate(url, UriKind.Absolute, out _);

    private bool MustBe12_24_36(int value)
        => value == 0 || value == 12 || value == 24 || value == 36;
    
}