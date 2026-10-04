using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        private readonly IProductCategoryLampshadeQuery _query;
        public ProductCategoryQueryModel ProductCategory;

        public ProductCategoryModel(IProductCategoryLampshadeQuery query)
        {
            _query = query;
        }

        public void OnGet(string id)
        {
            ProductCategory = _query.GetProductCategoryWithProductsBySlug(id);
        }
    }
}
