using _0_Framework.Infrastructure;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<long, ProductCategory> ,IProductCategoryRepository
    {
        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public string? GetSlugById(long id)
        {
            return _context.ProductCategories.Select(x => new {x.Id, x.Slug})
                .FirstOrDefault(x => x.Id == id)?.Slug;
        }
    }
}