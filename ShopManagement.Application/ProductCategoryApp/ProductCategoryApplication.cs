using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductCategoryApp;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.ProductCategoryApp
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _repository;
        private readonly IProductCategoryQuery _query;

        public ProductCategoryApplication(IProductCategoryRepository repository, IProductCategoryQuery query)
        {
            _repository = repository;
            _query = query;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();

            if (_repository.Exists(x=>x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            var category = new ProductCategory
            (
                command.Name,
                command.Description,
                command.Picture,
                command.PictureAlt,
                command.PictureTitle,
                command.Keywords,
                command.MetaDescription,
                slug
            );

            _repository.Create(category);
            _repository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var category = _repository.Get(command.Id);

            if (category == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x=>x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            
            var slug = command.Slug.Slugify();

            category.Edit
                (
                    command.Name,
                    command.Description,
                    command.Picture,
                    command.PictureAlt,
                    command.PictureTitle,
                    command.Keywords,
                    command.MetaDescription,
                    slug
                );

            _repository.SaveChanges();

            return operation.Succeeded();
        }

        public EditProductCategory? GetDetails(long id)
        {
            return _query.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _query.Search(searchModel);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _query.GetProductCategories();
        }
    }
}