namespace SoftwareCatalog.Api.Vendors;

public class VendorItemEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset DateTimeAdded { get; set; }
    public string Url { get; set; } = string.Empty;
}
