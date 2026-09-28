using _01_LampshadeQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class SlideViewComponent : ViewComponent
    {
        private readonly ISlideLampshadeQuery _query;

        public SlideViewComponent(ISlideLampshadeQuery slideQuery)
        {
            _query = slideQuery;
        }

        public IViewComponentResult Invoke()
        {
            var slides = _query.GetSlides();
            return View(slides);
        }
    }
}
