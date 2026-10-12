using _0_Framework.Application;
using CommentManagement.Application.Contract.CommentApp;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentQuery : ICommentQuery
    {
        private readonly CommentContext _context;

        public CommentQuery(CommentContext context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments
                .Select(x => new CommentViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Website = x.Website,
                Message = x.Message,
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
                OwnerRecordId = x.OwnerRecordId,
                Type = x.Type,
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
