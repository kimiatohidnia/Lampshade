using _0_Framework.Application;

namespace AccountManagement.Application.Contract.RoleApp
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command);
        OperationResult Edit(EditRole command);
        List<RoleViewModel> GetRoleList();
        EditRole? GetDetails(long id);
    }
}
