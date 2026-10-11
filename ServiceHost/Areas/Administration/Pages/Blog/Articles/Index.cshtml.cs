using BlogManagement.Application.Contract.ArticleApp;
using BlogManagement.Application.Contract.ArticleCategoryApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    [Area("Administration")]
    public class IndexModel : PageModel
    {
        public List<ArticleViewModel> Articles;
        public SelectList ArticleCategoriesSelectList;
        public ArticleSearchModel SearchModel;
        private readonly IArticleApplication _application;
        private readonly IArticleCategoryApplication _categoryApplication;

        public IndexModel(IArticleApplication application, IArticleCategoryApplication categoryApplication)
        {
            _application = application;
            _categoryApplication = categoryApplication;
        }

        public void OnGet(ArticleSearchModel searchModel)
        {
            ArticleCategoriesSelectList = new SelectList(_categoryApplication.GetCategories(), "Id", "Name");
            Articles = _application.Search(searchModel);
        }
    }
}
