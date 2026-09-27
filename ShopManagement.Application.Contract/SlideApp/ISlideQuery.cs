namespace ShopManagement.Application.Contract.SlideApp
{
    public interface ISlideQuery
    {
        EditSlide? GetDetails(long id);
        List<SlideViewModel> GetSlides();
    }
}
