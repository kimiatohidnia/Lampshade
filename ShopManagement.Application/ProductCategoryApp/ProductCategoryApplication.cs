using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductCategoryApp;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.ProductCategoryApp
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _repository;

        public ProductCategoryApplication(IProductCategoryRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();

            if (_repository.Exists(x=>x.Name == command.Name))
                return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد.");

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
                return operation.Failed("رکوردی با این اطلاعات یافت نشد!");

            if (_repository.Exists(x=>x.Name == command.Name && x.Id != command.Id))
                return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد.");
            
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

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            
        }
    }
}
