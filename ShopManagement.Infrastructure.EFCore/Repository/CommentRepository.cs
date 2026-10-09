using _0_Framework.Infrastructure;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        public CommentRepository(ShopContext context) : base(context)
        {
        }
    }
}
