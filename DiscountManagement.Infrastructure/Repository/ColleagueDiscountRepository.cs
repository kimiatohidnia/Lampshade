using _0_Framework.Infrastructure;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        public ColleagueDiscountRepository(DiscountContext context) : base(context)
        {
        }
    }
}
