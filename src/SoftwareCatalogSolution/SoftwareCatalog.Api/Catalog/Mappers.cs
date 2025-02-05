namespace SoftwareCatalog.Api.Catalog;

public static class CatalogMappingExtensions
{
    public static CatalogItemEntity ToCatalogItemEntity(this CatalogItemRequestModel model, string vendor, CatalogItemLicenceTypes licence)
    {
        return new CatalogItemEntity
        {
            Id = Guid.NewGuid(),
            Licence = licence,
            Name = model.Name,
            Vendor = vendor
        };
    }

    public static CatalogItemResponseDetailsModel ToCatalogDetailsResponseModel(this CatalogItemEntity entity)
    {
        return new CatalogItemResponseDetailsModel
        {
            Id = entity.Id,
            Licence = entity.Licence,
            Name = entity.Name,
            Vendor = entity.Vendor
        };
    }

}
