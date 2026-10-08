namespace _01_LampshadeQuery.Contracts.Product
{
    public interface IProductLampshadeQuery
    {
        ProductQueryModel GetProductBySlug(string slug);
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
    }
}
