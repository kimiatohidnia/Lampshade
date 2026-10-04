namespace _01_LampshadeQuery.Contracts.Product
{
    public interface IProductLampshadeQuery
    {
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
    }
}
