namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory entity);

        ProductCategory GetById(long id);

        List<ProductCategory> GetAll();
    }
}
