namespace DiscountManagement.Application.Contract.CustomerDiscountApp
{
    public interface ICustomerDiscountQuery
    {
        EditCustomerDiscount GetDetails(long id);
        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel  searchModel);
    }
}
