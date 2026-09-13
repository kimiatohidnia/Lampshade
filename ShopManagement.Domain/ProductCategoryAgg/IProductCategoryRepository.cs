using System.Linq.Expressions;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory entity);
        void SaveChanges();
        bool Exists(Expression<Func<ProductCategory, bool>> expression);
        ProductCategory? Get(long id);
        List<ProductCategory> GetAll();
    }
}
