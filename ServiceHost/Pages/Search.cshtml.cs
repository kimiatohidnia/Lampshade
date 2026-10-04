using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IProductLampshadeQuery _query;
        public List<ProductQueryModel> Products;
        public string Value;

        public SearchModel(IProductLampshadeQuery query)
        {
            _query = query;
        }

        public void OnGet(string value)
        {
            Value = value;
            Products = _query.Search(value);
        }
    }
}
