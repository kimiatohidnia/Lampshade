using _0_Framework.Infrastructure;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly BlogContext _context;
        public ArticleRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public Article GetArticleWithCategoryById(long id)
        {
            return _context.Articles.Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
