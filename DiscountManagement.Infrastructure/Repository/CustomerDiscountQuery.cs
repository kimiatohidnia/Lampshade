using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscountApp;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountQuery : ICustomerDiscountQuery
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;

        public CustomerDiscountQuery(DiscountContext context, ShopContext shopContext)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(x => new EditCustomerDiscount
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    DiscountRate = x.DiscountRate,
                    StartDate = x.StartDate.ToString(),
                    EndDate = x.EndDate.ToString(),
                    Reason = x.Reason
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();

            var query = _context.CustomerDiscounts
                .Select(x => new CustomerDiscountViewModel
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    DiscountRate = x.DiscountRate,
                    StartDate = x.StartDate.ToFarsi(),
                    EndDate = x.EndDate.ToFarsi(),
                    StartDateGr = x.StartDate,
                    EndDateGr = x.EndDate,
                    Reason = x.Reason,
                    CreationDate = x.CreationDate.ToFarsi()
                });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
                query = query.Where(x => x.StartDateGr > searchModel.StartDate.ToGeorgianDateTime());

            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
                query = query.Where(x => x.EndDateGr < searchModel.EndDate.ToGeorgianDateTime());

            var discounts = query.OrderByDescending(x => x.Id).ToList();

            discounts.ForEach(discounts =>
                discounts.Product = products.FirstOrDefault(x=> x.Id == discounts.ProductId)?.Name);

            return discounts;
        }
    }
}
