using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query
{
    public class ProductCategoryLampshadeQuery : IProductCategoryLampshadeQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductCategoryLampshadeQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _shopContext.ProductCategories
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                }).AsNoTracking().ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.UnitPrice })
                .ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate })
                .ToList();

            var categories = _shopContext.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products)
                }).AsNoTracking().ToList();

            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    var productInventory = inventory
                        .FirstOrDefault(x => x.ProductId == product.Id);

                    if (productInventory != null)
                    {
                        var price = productInventory.UnitPrice;

                        product.Price = price.ToMoney();

                        var discount = discounts
                        .FirstOrDefault(x => x.ProductId == product.Id);

                        if (discount != null)
                        {
                            int discountRate = discount.DiscountRate;

                            product.DiscountRate = discountRate;

                            product.HasDiscount = discountRate > 0;

                            var discountAmount = Math.Round((price * discountRate) / 100);

                            product.PriceWithDiscount = (price - discountAmount).ToMoney();
                        }
                    }
                }
            }

            return categories;
        }

        public static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products.Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).OrderByDescending(x => x.Id).ToList();
        }

        public ProductCategoryQueryModel GetProductCategoryWithProductsBySlug(string slug)
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.UnitPrice });

            var discount = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate });

            var category = _shopContext.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Products = MapProducts(x.Products)
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            foreach (var product in category.Products)
            {
                var productinventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);

                if (productinventory != null)
                {
                    var price = productinventory.UnitPrice;

                    product.Price = price.ToMoney();

                    var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id);

                    if (productDiscount != null)
                    {
                        int discountRate = productDiscount.DiscountRate;

                        product.DiscountRate = discountRate;

                        product.DiscountEndDate = productDiscount.EndDate.ToDiscountFormat();

                        product.HasDiscount = discountRate > 0;

                        var discountAmount = Math.Round((price * discountRate) / 100);

                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }
            }

            return category;
        }
    }
}
