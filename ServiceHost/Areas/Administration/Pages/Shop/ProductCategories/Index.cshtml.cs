using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.ProductCategoryApp;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    [Area("Administration")]
    public class IndexModel : PageModel
    {
        public List<ProductCategoryViewModel> ProductCategories;
        private readonly IProductCategoryApplication _application;

        public IndexModel(IProductCategoryApplication application)
        {
            _application = application;
        }

        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _application.Search(searchModel);
        }
    }
}
