using FluentValidation;

namespace SoftwareCatalog.Api.Vendors;

public record VendorCreateModel
{
    public string Name { get; set; } = string.Empty;
    public string? Link { get; set; } = string.Empty;
}

public class VendorCreateModelValidator : AbstractValidator<VendorCreateModel>
{
    public VendorCreateModelValidator()
    {
        RuleFor(v => v.Name).NotEmpty().MinimumLength(2).MaximumLength(100);
        RuleFor(v => v.Link).Matches("^https://(.*)").When(v => !string.IsNullOrEmpty(v.Link));
    }
}
public record VendorDetailsResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Link { get; set; }
    public DateTimeOffset CreatedOn { get; set; }

}

