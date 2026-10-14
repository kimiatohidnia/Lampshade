namespace AccountManagement.Application.Contract.RoleApp
{
    public interface IRoleQuery
    {
        List<RoleViewModel> GetRoleList();
        EditRole? GetDetails(long id);
    }
}
