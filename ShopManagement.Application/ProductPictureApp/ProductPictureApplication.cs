using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductPictureApp;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application.ProductPictureApp
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _repository;
        private readonly IProductPictureQuery _query;
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;

        public ProductPictureApplication(IProductPictureQuery query, IProductPictureRepository repository, IProductRepository productRepository, IFileUploader fileUploader)
        {
            _query = query;
            _repository = repository;
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();

            var product = _productRepository.GetProductWithCategory(command.ProductId);

            var picturePath = $"{product.Category.Slug}/{product.Slug}";
            var pictureName = _fileUploader.Upload(command.Picture, picturePath);

            var productPicture = new ProductPicture
            (
                command.ProductId,
                pictureName,
                command.PictureAlt,
                command.PictureTitle
            );

            _repository.Create(productPicture);
            _repository.SaveChanges(); 
            
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _repository.GetProductPictureWithProductAndCategory(command.Id);

            if(productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            var picturePath = $"{productPicture.Product.Category.Slug}/{productPicture.Product.Slug}";
            var pictureName = _fileUploader.Upload(command.Picture, picturePath);

            productPicture.Edit
            (
                command.ProductId,
                pictureName,
                command.PictureAlt,
                command.PictureTitle
            );
            
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operation= new OperationResult();
            var productPicture = _repository.Get(id);

            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Remove();
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productPicture = _repository.Get(id);

            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Restore();
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditProductPicture? GetDetails(long id)
        {
            return _query.GetDetails(id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _query.Search(searchModel);
        }
    }
}
