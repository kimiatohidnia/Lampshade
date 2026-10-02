using ShopManagement.Application.Contract.ProductApp;

namespace InventoryManagement.Application.Contract.InventoryApp
{
    public class CreateInventory
    {
        public long ProductId { get; set; }
        public double UnitPrice { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
