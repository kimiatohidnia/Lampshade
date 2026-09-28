using ShopManagement.Application.Contract.SlideApp;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }

        public EditSlide? GetDetails(long id)
        {
            return _context.Slides.Select(x=> new EditSlide
            {
                Id = x.Id,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Heading = x.Heading,
                Title = x.Title,
                Text = x.Text,
                BtnText = x.BtnText,
                Link = x.Link
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<SlideViewModel> GetSlides()
        {
            return _context.Slides.Select(x=> new SlideViewModel
            {
                Id = x.Id,
                Picture = x.Picture,
                Heading = x.Heading,
                Title = x.Title,
                CreationDate = x.CreationDate.ToString(),
                IsRemoved = x.IsRemoved
            }).OrderByDescending(x=> x.Id).ToList();
        }
    }
}
