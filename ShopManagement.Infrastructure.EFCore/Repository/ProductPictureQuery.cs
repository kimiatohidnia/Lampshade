using ShopManagement.Application.Contract.ProductPictureApp;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureQuery : IProductPictureQuery
    {
        private readonly ShopContext _context;

        public ProductPictureQuery(ShopContext context)
        {
            _context = context;
        }

        public EditProductPicture? GetDetails(long id)
        {
            return _context.ProductPictures.Select(x=> new EditProductPicture
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures.Select(x => new ProductPictureViewModel
            {
                Id = x.Id,
                Picture = x.Picture,
                Product = x.Product.Name,
                ProductId = x.ProductId,
                CreationDate = x.CreationDate.ToString(),
                IsRemoved = x.IsRemoved
            });

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
