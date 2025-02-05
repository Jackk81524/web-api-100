namespace SoftwareCatalog.Api.Catalog;

public record CatalogItemRequestModel
{
    public string Name { get; set; } = string.Empty;

}


public record CatalogItemResponseDetailsModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Vendor { get; set; } = string.Empty;
    public CatalogItemLicenceTypes Licence { get; set; }

}