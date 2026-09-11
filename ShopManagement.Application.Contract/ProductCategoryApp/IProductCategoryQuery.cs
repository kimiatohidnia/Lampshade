namespace ShopManagement.Application.Contract.ProductCategoryApp
{
    public interface IProductCategoryQuery
    {
        ProductCategoryViewModel GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
