//namespace _0_Framework
//{
//    public class AplicationQuery
//    {
//    // ShopManagement.Infrastructure/EFCore/ProductCategoryQuery.cs
//    using ShopManagement.Application.Contract.ProductCategoryApp;

//    namespace ShopManagement.Infrastructure.EFCore
//    {
//        public class ProductCategoryQuery : IProductCategoryQuery
//        {
//            private readonly ShopContext _context;

//            public ProductCategoryQuery(ShopContext context)
//            {
//                _context = context;
//            }

//            public EditProductCategory GetDetails(long id)
//            {
//                return _context.ProductCategories
//                    .Select(x => new EditProductCategory
//                    {
//                        Id = x.Id,
//                        Name = x.Name,
//                        Description = x.Description,
//                        Picture = x.Picture,
//                        PictureAlt = x.PictureAlt,
//                        PictureTitle = x.PictureTitle,
//                        Keywords = x.Keywords,
//                        MetaDescription = x.MetaDescription,
//                        Slug = x.Slug
//                    })
//                    .FirstOrDefault(x => x.Id == id);
//            }

//            public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
//            {
//                var query = _context.ProductCategories.Select(x => x);

//                if (!string.IsNullOrWhiteSpace(searchModel.Name))
//                    query = query.Where(x => x.Name.Contains(searchModel.Name));

//                return query.Select(x => new ProductCategoryViewModel
//                {
//                    Id = x.Id,
//                    Name = x.Name,
//                    Picture = x.Picture,
//                    CreationDate = x.CreationDate.ToFarsiDate(), // مثلاً
//                    ProductCount = 0 // بعداً با Join واقعی پر می‌شه
//                }).ToList();
//            }
//        }
//    }
//}
//}
