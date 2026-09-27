namespace ShopManagement.Application.Contract.ProductPictureApp
{
    public interface IProductPictureQuery
    {
        EditProductPicture? GetDetails(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
