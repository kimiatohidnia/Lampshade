using _0_Framework.Application;
using InventoryManagement.Application.Contract.InventoryApp;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryQuery _query;
        private readonly IInventoryRepository _repository;

        public InventoryApplication(IInventoryQuery query, IInventoryRepository repository)
        {
            _repository = repository;
            _query = query;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();

            if(_repository.Exists(x => x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var inventory = new Inventory(command.ProductId, command.UnitPrice);

            _repository.Create(inventory);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _repository.Get(command.Id);

            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if(_repository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            inventory.Edit(command.ProductId, command.UnitPrice);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditInventory? GetDetails(long id)
        {
            return _query.GetDetails(id);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            return _query.GetOperationLog(inventoryId);
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _repository.Get(command.InventoryId);

            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            const long operatorId = 1;

            inventory.Increase(command.Count, operatorId, command.Description);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operation = new OperationResult();
            const long operatorId = 1;

            foreach (var item in command)
            {
                var inventory = _repository.Get(item.InventoryId);

                if (inventory == null)
                    return operation.Failed(ApplicationMessages.RecordNotFound);

                inventory.Reduce(item.Count, operatorId, item.Description, item.OrderId);
            }

            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = _repository.Get(command.InventoryId);

            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            const long operatorId = 1;

            inventory.Reduce(command.Count, operatorId, command.Description, 0);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _query.Search(searchModel);
        }
    }
}
