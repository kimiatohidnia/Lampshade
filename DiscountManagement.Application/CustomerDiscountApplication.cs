using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscountApp;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _repository;
        private readonly ICustomerDiscountQuery _query;

        public CustomerDiscountApplication(ICustomerDiscountRepository repository, ICustomerDiscountQuery query)
        {
            _repository = repository;
            _query = query;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();

            if (_repository.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            var customerDiscount = new CustomerDiscount(command.ProductId, command.DiscountRate,
                startDate, endDate, command.Reason);

            _repository.Create(customerDiscount);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var customerDiscount = _repository.Get(command.Id);

            if (customerDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x => x.ProductId == command.ProductId 
                                        && x.DiscountRate == command.DiscountRate 
                                        && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            customerDiscount.Edit(command.ProductId, command.DiscountRate, startDate,
                endDate, command.Reason);

            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _query.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _query.Search(searchModel);
        }
    }
}
