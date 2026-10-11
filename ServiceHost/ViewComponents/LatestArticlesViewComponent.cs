using _01_LampshadeQuery.Contracts.Article;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArticlesViewComponent : ViewComponent
    {
        private readonly IArticleLampshadeQuery _query;

        public LatestArticlesViewComponent(IArticleLampshadeQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var latestArticle = _query.LatestArticles();
            return View(latestArticle);
        }
    }
}
