using BlogManagement.Application.Contract.ArticleApp;
using BlogManagement.Application.Contract.ArticleCategoryApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class CreateModel : PageModel
    {
        public CreateArticle command;
        public SelectList Categories;
        private readonly IArticleCategoryApplication _categoryApplication;
        private readonly IArticleApplication _application;

        public CreateModel(IArticleCategoryApplication categoryApplication, IArticleApplication application)
        {
            _categoryApplication = categoryApplication;
            _application = application;
        }

        public void OnGet()
        {
            Categories = new SelectList(_categoryApplication.GetCategories(), "Id", "Name");
        }

        public IActionResult OnPost(CreateArticle command)
        {
            var result = _application.Create(command);

            return RedirectToPage("./Index");
        }
    }
}
