using Riok.Mapperly.Abstractions;

namespace SoftwareCatalog.Api.Vendors;

[Mapper]
public static partial class VendorsMappers
{
    public static partial IQueryable<VendorItemResponseDetailsModel> ProjectToDetailsModel(this IQueryable<VendorItemEntity> model);
    public static partial VendorItemResponseDetailsModel ToVendorItemResponseModel(this VendorItemEntity model);
}
