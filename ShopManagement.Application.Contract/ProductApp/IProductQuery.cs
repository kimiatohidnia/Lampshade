namespace ShopManagement.Application.Contract.ProductApp
{
    public interface IProductQuery
    {
        EditProduct? GetDetails(long id);
        List<ProductViewModel> Search(ProductSearchModel  searchModel);
    }
}
