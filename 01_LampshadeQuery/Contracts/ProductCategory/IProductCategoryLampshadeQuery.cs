namespace _01_LampshadeQuery.Contracts.ProductCategory
{
    public interface IProductCategoryLampshadeQuery
    {
        List<ProductCategoryQueryModel> GetProductCategories();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
        ProductCategoryQueryModel GetProductCategoryWithProductsBySlug(string slug);
    }
}
