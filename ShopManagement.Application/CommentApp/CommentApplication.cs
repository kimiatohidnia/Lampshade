using _0_Framework.Application;
using ShopManagement.Application.Contract.CommentApp;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Application.CommentApp
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _repository;
        private readonly ICommentQuery _query;

        public CommentApplication(ICommentRepository repository, ICommentQuery query)
        {
            _repository = repository;
            _query = query;
        }

        public OperationResult Add(AddComment command)
        {
            var operation = new OperationResult();

            var comment = new Comment(command.Name, command.Email, command.Message, command.ProductId);

            _repository.Create(comment);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();

            var comment = _repository.Get(id);

            if (comment == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            comment.Cancel();
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Confirm(long id)
        {
            var operation = new OperationResult();

            var comment = _repository.Get(id);

            if (comment == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            comment.Confirm();
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _query.Search(searchModel);
        }
    }
}
