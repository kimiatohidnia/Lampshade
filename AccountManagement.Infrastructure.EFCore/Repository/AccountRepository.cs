using _0_Framework.Infrastructure;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public Account? GetBy(string username)
        {
            return _context.Accounts
                .FirstOrDefault(x => x.Username == username);
        }
    }
}
