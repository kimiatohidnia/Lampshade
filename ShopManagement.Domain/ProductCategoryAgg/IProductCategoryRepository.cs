using System.Linq.Expressions;
using ShopManagement.Application.Contract.ProductCategoryApp;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory entity);
        void SaveChanges();
        bool Exists(Expression<Func<ProductCategory, bool>> expression);
        ProductCategory? Get(long id);
        List<ProductCategory> GetAll();
        EditProductCategory? GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
