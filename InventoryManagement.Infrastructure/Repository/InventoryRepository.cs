using _0_Framework.Infrastructure;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext _context;
        public InventoryRepository(InventoryContext context) : base(context)
        {
            _context = context;
        }

        public Inventory? GetById(long productId)
        {
            return _context.Inventory.FirstOrDefault(x=> x.ProductId == productId);
        }
    }
}
