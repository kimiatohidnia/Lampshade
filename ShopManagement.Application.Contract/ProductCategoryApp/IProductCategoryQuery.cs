namespace ShopManagement.Application.Contract.ProductCategoryApp
{
    public interface IProductCategoryQuery
    {
        EditProductCategory? GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
        //List<ProductCategoryViewModel> GetProductCategories();
    }
}
