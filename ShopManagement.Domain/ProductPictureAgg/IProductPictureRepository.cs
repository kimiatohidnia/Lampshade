using _0_Framework.Domain;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long, ProductPicture>
    {
        ProductPicture? GetProductPictureWithProductAndCategory(long id);
    }
}
