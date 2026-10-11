using BlogManagement.Application.Contract.ArticleApp;
using BlogManagement.Application.Contract.ArticleCategoryApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class EditModel : PageModel
    {
        public EditArticle Command;
        public SelectList Categories;
        private readonly IArticleCategoryApplication _categoryApplication;
        private readonly IArticleApplication _application;

        public EditModel(IArticleCategoryApplication categoryApplication, IArticleApplication application)
        {
            _categoryApplication = categoryApplication;
            _application = application;
        }

        public void OnGet(long id)
        {
            Command = _application.GetDetails(id);
            Categories = new SelectList(_categoryApplication.GetCategories(), "Id", "Name");
        }

        public IActionResult OnPost(EditArticle command)
        {
            var result = _application.Edit(command);
            return RedirectToPage("./Index");
        }
    }
}