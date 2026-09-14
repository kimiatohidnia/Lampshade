using _0_Framework.Application;

namespace ShopManagement.Application.Contract.ProductCategoryApp
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);
        OperationResult Edit(EditProductCategory  command);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
