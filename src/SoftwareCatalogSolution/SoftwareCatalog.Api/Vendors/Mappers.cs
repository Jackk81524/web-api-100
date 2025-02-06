using Riok.Mapperly.Abstractions;

namespace SoftwareCatalog.Api.Vendors;


[Mapper]
public static partial class VendorMappers
{
    public static partial IQueryable<VendorDetailsResponseModel> ProjectToModel(this IQueryable<VendorEntity> entity);
    public static partial VendorDetailsResponseModel MapToModel(this VendorEntity entity);
}
