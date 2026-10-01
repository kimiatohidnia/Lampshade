using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<long, Inventory>
    {
        Inventory? GetById(long productId);
    }
}
