using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductPictureApp;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application.ProductPictureApp
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _repository;
        private readonly IProductPictureQuery _query;

        public ProductPictureApplication(IProductPictureQuery query, IProductPictureRepository repository)
        {
            _query = query;
            _repository = repository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();

            if (_repository.Exists(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var productPicture = new ProductPicture(command.ProductId, command.Picture, command.PictureAlt,
                command.PictureTitle);

            _repository.Create(productPicture);
            _repository.SaveChanges(); 
            
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _repository.Get(command.Id);

            if(productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            
            if (_repository.Exists(x=> x.Picture == command.Picture && x.ProductId == command.ProductId && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            productPicture.Edit(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle);
            
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
