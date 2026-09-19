using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductApp;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application.ProductApp
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _repository;
        private readonly IProductQuery _query;

        public ProductApplication(IProductQuery query, IProductRepository repository)
        {
            _query = query;
            _repository = repository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();

            if (_repository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            var product = new Product
                (
                    command.Name,
                    command.Code,
                    command.UnitPrice,
                    command.ShortDescription,
                    command.Description,
                    command.Picture,
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
            var product = _repository.Get(command.Id);

            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            product.Edit
            (
                command.Name,
                command.Code,
                command.UnitPrice,
                command.ShortDescription,
                command.Description,
                command.Picture,
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

        public OperationResult InStock(long id)
        {
            var operation = new OperationResult();
            var product = _repository.Get(id);

            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            product.InStock();
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult NotInStock(long id)
        {
            var operation = new OperationResult();
            var product = _repository.Get(id);

            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            product.NotInStock();
            _repository.SaveChanges();
            return operation.Succeeded();
        }
    }
}
