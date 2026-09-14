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
        private readonly IProductCategoryQuery _query;

        public IndexModel(IProductCategoryApplication application, IProductCategoryQuery query)
        {
            _application = application;
            _query = query;
        }

        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _query.Search(searchModel);
        }
    }
}
