using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.ProductCategoryApp;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    [Area("Administration")]
    public class IndexModel : PageModel
    {
        public List<ProductCategoryViewModel> ProductCategories;
        public ProductCategorySearchModel SearchModel;
        private readonly IProductCategoryApplication _application;

        public IndexModel(IProductCategoryApplication application)
        {
            _application = application;
        }

        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _application.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _application.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productCategory = _application.GetDetails(id);
            return Partial("./Edit", productCategory);
        }

        public JsonResult OnPostEdit(EditProductCategory command)
        {
            var result = _application.Edit(command);
            return new JsonResult(result);
        }
    }
}
