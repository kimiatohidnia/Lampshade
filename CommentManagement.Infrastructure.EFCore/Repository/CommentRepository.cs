using _0_Framework.Infrastructure;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        public CommentRepository(CommentContext context) : base(context)
        {
        }
    }
}
