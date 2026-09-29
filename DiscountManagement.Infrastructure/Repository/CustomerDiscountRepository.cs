using _0_Framework.Infrastructure;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        public CustomerDiscountRepository(DiscountContext context) : base(context)
        {
        }
    }
}
