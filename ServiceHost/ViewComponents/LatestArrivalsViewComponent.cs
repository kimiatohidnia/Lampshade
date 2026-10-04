using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArrivalsViewComponent : ViewComponent
    {
        private readonly IProductLampshadeQuery _query;

        public LatestArrivalsViewComponent(IProductLampshadeQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var products = _query.GetLatestArrivals();
            return View(products);
        }
    }
}
