namespace DiscountManagement.Application.Contract.ColleagueDiscountApp
{
    public class DefineColleagueDiscount
    {
        public long ProductId { get; set; }
        public int DiscountRate { get; set; }
    }

    public class EditColleagueDiscount : DefineColleagueDiscount
    {
        public long Id { get; set; }
    }

    public class ColleagueDiscountViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public int DiscountRate { get; set; }
    }

    public class ColleagueDiscountSearchModel
    {
        public long ProductId { get; set; }
    }
}
