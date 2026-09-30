namespace DiscountManagement.Application.Contract.ColleagueDiscountApp
{
    public interface IColleagueDiscountQuery
    {
        EditColleagueDiscount? GetDetails(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    }
}
