using _0_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscountApp;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _repository;
        private readonly IColleagueDiscountQuery _query;

        public ColleagueDiscountApplication(IColleagueDiscountRepository repository, IColleagueDiscountQuery query)
        {
            _repository = repository;
            _query = query;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();

            if (_repository.Exists(x => x.ProductId == command.ProductId &&
                                        x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var discount = new ColleagueDiscount(command.ProductId, command.DiscountRate);

            _repository.Create(discount);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var discount = _repository.Get(command.Id);

            if (discount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x => x.ProductId == command.ProductId &&
                                        x.DiscountRate == command.DiscountRate && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            discount.Edit(command.ProductId, command.DiscountRate);

            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var discount = _repository.Get(id);

            if (discount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            discount.Remove();

            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var discount = _repository.Get(id);

            if (discount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            discount.Restore();

            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditColleagueDiscount? GetDetails(long id)
        {
            return _query.GetDetails(id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _query.Search(searchModel);
        }
    }
}
