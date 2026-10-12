using _01_LampshadeQuery.Contracts.Product;
using CommentManagement.Application.Contract.CommentApp;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductLampshadeQuery _query;
        private readonly ICommentApplication _application;
        public ProductQueryModel Product;

        public ProductModel(IProductLampshadeQuery query, ICommentApplication application)
        {
            _query = query;
            _application = application;
        }

        public void OnGet(string id)
        {
            Product = _query.GetProductBySlug(id);
        }

        public IActionResult OnPost(AddComment command, string productSlug)
        {
            command.Type = CommentType.Product;

            var result = _application.Add(command);

            return RedirectToPage("/Product", new { Id = productSlug });
        }
    }
}
