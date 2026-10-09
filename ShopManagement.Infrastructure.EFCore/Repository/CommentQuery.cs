using _0_Framework.Application;
using ShopManagement.Application.Contract.CommentApp;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class CommentQuery : ICommentQuery
    {
        private readonly ShopContext _context;

        public CommentQuery(ShopContext context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments.Select(x => new CommentViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Message = x.Message,
                ProductName = x.Product.Name,
                ProductId = x.Product.Id,
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
                CommentDate = x.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
