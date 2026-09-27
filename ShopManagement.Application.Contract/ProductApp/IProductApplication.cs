using _0_Framework.Application;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application.Contract.ProductApp
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct  command);
        OperationResult InStock(long id);
        OperationResult NotInStock(long id);
        EditProduct? GetDetails(long id);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        List<ProductViewModel> GetProducts();
    }
}
