using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.Contract.ProductCategoryApp
{
    public interface IProductCategoryApplication
    {
        void Create(CreateProductCategory command);
        void Edit(EditProductCategory  command);

        ProductCategory GetDetails(long id);

        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
