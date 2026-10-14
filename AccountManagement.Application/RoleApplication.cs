using _0_Framework.Application;
using AccountManagement.Application.Contract.RoleApp;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _repository;
        private readonly IRoleQuery _query;

        public RoleApplication(IRoleRepository repository, IRoleQuery query)
        {
            _repository = repository;
            _query = query;
        }

        public OperationResult Create(CreateRole command)
        {
            var operation = new OperationResult();

            if (_repository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var role = new Role(command.Name);

            _repository.Create(role);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();

            var role = _repository.Get(command.Id);

            if (role == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            role.Edit(command.Name);

            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditRole? GetDetails(long id)
        {
            return _query.GetDetails(id);
        }

        public List<RoleViewModel> GetRoleList()
        {
            return _query.GetRoleList();
        }
    }
}
