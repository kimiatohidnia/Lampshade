using _0_Framework.Infrastructure;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
    {
        public ArticleCategoryRepository(BlogContext context) : base(context)
        {
        }
    }
}
