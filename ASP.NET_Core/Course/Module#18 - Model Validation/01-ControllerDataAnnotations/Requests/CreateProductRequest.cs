
using System.ComponentModel.DataAnnotations;
using ControllerDataAnnotations.Enums;
using ControllerDataAnnotations.Validators;

namespace ControllerDataAnnotations.Requests;

public class CreateProductRequest
{
    [Required(ErrorMessage = "Product Name is required")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "Product Name must be between 3 and 255 characters")]
    public string Name {get; set;} = null!;
    

    [StringLength(1000, ErrorMessage = "Description can't exceed 1000 characters")]
    public string? Description {get; set;}
    
    
    [Required(ErrorMessage = "Product SKU is required")]
    [RegularExpression(@"^PRD-\d{5}$", ErrorMessage = "SKU must be 'PRD-' followed by 5 digits 'PRD-XXXXX'" )]
    public string? SKU {get; set;}    


    [Required(ErrorMessage = "Product Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be at least 0.01")]
    public decimal Price {get; set;}
    

    [Required(ErrorMessage = "Product Stock Quantity is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity can't be negative")]
    public int StockQuantity {get; set;}
    

    [Required(ErrorMessage = "Product Launch Date is required")]
    [CustomValidation(typeof(LaunchDateValidator), nameof(LaunchDateValidator.MustBeTodayOrFuture))]
    public DateTime LaunchDate {get; set;}
    

    [Required(ErrorMessage = "Product Category is required")]
    [EnumDataType(typeof(ProductCategory), ErrorMessage = "Invalid product category")]
    public ProductCategory Category {get; set;}
    

    [Url(ErrorMessage = "Image Url is not valid")]
    public string? ImageUrl {get; set;}


    [Required(ErrorMessage = "Product Weight is required")]
    [Range(0.01, 1000, ErrorMessage = "Weight must be between 0.01 and 1000 kg")]
    public decimal Weight {get; set;}
    

    [CustomValidation(typeof(WarrantyValidator), nameof(WarrantyValidator.MustBe12_24_36))]
    public int WarrantyPeriodMonths {get; set;}
    

    [Required(ErrorMessage = "Product Returnable state is required")]
    public bool IsReturnable {get; set;}
    

    [RequiredIf(nameof(IsReturnable), true, ErrorMessage = "ReturnPolicyDescription is required")]
    public string? ReturnPolicyDescription {get; set;}
    

    [MaxLength(5, ErrorMessage = "A maximum of 5 tags is allowed")]
    public List<string> Tags {get; set;} = null!;
}
