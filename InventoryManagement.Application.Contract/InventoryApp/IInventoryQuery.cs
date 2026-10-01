namespace InventoryManagement.Application.Contract.InventoryApp
{
    public interface IInventoryQuery
    {
        EditInventory? GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
    }
}
