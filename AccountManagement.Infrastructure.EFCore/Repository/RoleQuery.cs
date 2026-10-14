using _0_Framework.Application;
using AccountManagement.Application.Contract.RoleApp;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleQuery : IRoleQuery
    {
        private readonly AccountContext _context;

        public RoleQuery(AccountContext context)
        {
            _context = context;
        }

        public EditRole? GetDetails(long id)
        {
            return _context.Roles
                .Select(x => new EditRole
                {
                    Id = x.Id,
                    Name = x.Name
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<RoleViewModel> GetRoleList()
        {
            return _context.Roles
                .Select(x => new RoleViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreationDate = x.CreationDate.ToFarsi()
                }).ToList();
        }
    }
}
