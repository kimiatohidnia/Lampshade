using System.Linq.Expressions;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context)
        {
            _context = context;
        }

        public void Create(ProductCategory entity)
        {
            _context.ProductCategories.Add(entity);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool Exists(Expression<Func<ProductCategory, bool>> expression)
        {
            return _context.ProductCategories.Any(expression);
        }

        public ProductCategory? Get(long id)
        {
            return _context.ProductCategories
                .Find(id);
        }

        public List<ProductCategory> GetAll()
        {
            return _context.ProductCategories.ToList();
        }
    }
}
