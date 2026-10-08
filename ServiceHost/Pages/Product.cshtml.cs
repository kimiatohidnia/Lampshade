using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductLampshadeQuery _query;
        public ProductQueryModel Product;

        public ProductModel(IProductLampshadeQuery query)
        {
            _query = query;
        }

        public void OnGet(string id)
        {
            Product = _query.GetProductBySlug(id);
        }
    }
}
