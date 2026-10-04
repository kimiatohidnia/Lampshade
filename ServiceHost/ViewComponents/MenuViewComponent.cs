using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryLampshadeQuery _query;

        public MenuViewComponent(IProductCategoryLampshadeQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
