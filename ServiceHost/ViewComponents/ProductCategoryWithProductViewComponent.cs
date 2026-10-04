using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductViewComponent : ViewComponent
    {
        private readonly IProductCategoryLampshadeQuery _query;

        public ProductCategoryWithProductViewComponent(IProductCategoryLampshadeQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _query.GetProductCategoriesWithProducts();
            return View(categories);
        }
    }
}
