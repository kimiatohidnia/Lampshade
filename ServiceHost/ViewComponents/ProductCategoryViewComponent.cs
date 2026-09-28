using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private readonly IProductCategoryLampshadeQuery _query;

        public ProductCategoryViewComponent(IProductCategoryLampshadeQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _query.GetProductCategories();
            return View(categories);
        }
    }
}
