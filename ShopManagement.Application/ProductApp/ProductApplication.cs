using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductApp;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.ProductApp
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _repository;
        private readonly IProductQuery _query;
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductApplication(IProductQuery query, IProductRepository repository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _query = query;
            _repository = repository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();

            if (_repository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            var categorySlug = _productCategoryRepository.GetSlugById(command.CategoryId);
            var picturePath = $"{categorySlug}/{slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);

            var product = new Product
                (
                    command.Name,
                    command.Code,
                    command.ShortDescription,
                    command.Description,
                    fileName,
                    command.PictureAlt,
                    command.PictureTitle,
                    command.Keywords,
                    command.MetaDescription,
                    slug,
                    command.CategoryId
                );

            _repository.Create(product);
            _repository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _repository.GetProductWithCategory(command.Id);

            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            var filePath = $"{product.Category.Slug}/{slug}";
            var fileName = _fileUploader.Upload(command.Picture, filePath);

            product.Edit
            (
                command.Name,
                command.Code,
                command.ShortDescription,
                command.Description,
                fileName,
                command.PictureAlt,
                command.PictureTitle,
                command.Keywords,
                command.MetaDescription,
                slug,
                command.CategoryId
            );

            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditProduct? GetDetails(long id)
        {
            return _query.GetDetails(id);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _query.Search(searchModel);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _query.GetProducts();
        }
    }
}
