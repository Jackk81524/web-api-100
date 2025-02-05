using FluentValidation;

namespace SoftwareCatalog.Api.Vendors;

public record VendorItemRequestModel
{
    public required string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

}

public class VendorItemRequestModelValidator : AbstractValidator<VendorItemRequestModel>
{
    public VendorItemRequestModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MinimumLength(2);
        RuleFor(x => x.Name).MaximumLength(100);

        RuleFor(x => x.Url).Matches(@"^(https://.*)?$");
    }
}


public record VendorItemResponseDetailsModel(Guid Id, string Name, DateTimeOffset DateTimeAdded)
{
    //public required Guid Id { get; set; }
    //public required string Name { get; set; } = string.Empty;
    //public required DateTimeOffset DateTimeAdded { get; set; }
    public string Url { get; set; } = string.Empty;
}
