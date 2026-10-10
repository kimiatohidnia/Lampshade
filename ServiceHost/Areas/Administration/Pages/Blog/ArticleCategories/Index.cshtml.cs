using BlogManagement.Application.Contract.ArticleCategoryApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategories
{
    [Area("Administration")]
    public class IndexModel : PageModel
    {
        public List<ArticleCategoryViewModel> ArticleCategories;
        public ArticleCategorySearchModel SearchModel;
        private readonly IArticleCategoryApplication _application;

        public IndexModel(IArticleCategoryApplication application)
        {
            _application = application;
        }

        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategories = _application.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateArticleCategory());
        }

        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
            var result = _application.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var ArticleCategory = _application.GetDetails(id);
            return Partial("./Edit", ArticleCategory);
        }

        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            var result = _application.Edit(command);
            return new JsonResult(result);
        }
    }
}
