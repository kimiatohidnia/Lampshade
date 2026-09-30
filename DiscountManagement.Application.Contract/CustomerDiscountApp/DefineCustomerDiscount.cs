using ShopManagement.Application.Contract.ProductApp;

namespace DiscountManagement.Application.Contract.CustomerDiscountApp
{
    public class DefineCustomerDiscount
    {
        public long ProductId { get; set; }
        public int DiscountRate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Reason { get; set; }
        public List<ProductViewModel> Products  { get; set; }
    }
}
