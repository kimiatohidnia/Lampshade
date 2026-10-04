using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Migrations;

namespace _01_LampshadeQuery.Query
{
    public class ProductLampshadeQuery : IProductLampshadeQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductLampshadeQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new {x.ProductId, x.UnitPrice})
                .ToList();

            var discount = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now &&  x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate })
                .ToList();

            var products = _shopContext.Products.Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug
            }).AsNoTracking().OrderByDescending(x => x.Id).Take(6).ToList();

            
            foreach (var product in products)
            {
                var productInventory = inventory
                    .FirstOrDefault(x => x.ProductId == product.Id);

                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;

                    product.Price = price.ToMoney();

                    var productDiscount = discount
                        .FirstOrDefault(x => x.ProductId == product.Id);

                    if (productDiscount != null)
                    {
                        int discountRate = productDiscount.DiscountRate;

                        product.DiscountRate = discountRate;

                        product.HasDiscount = discountRate > 0;

                        var discountAmount = Math.Round((price * discountRate) / 100);
                        
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }
            }

            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.UnitPrice });

            var discount = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate });

            var query = _shopContext.Products
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    CategorySlug = x.Category.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    ShortDescription = x.ShortDescription
                }).AsNoTracking();

            if(!string.IsNullOrWhiteSpace(value))
                query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));

            var products = query.OrderByDescending(x => x.Id).ToList();

            foreach (var product in products)
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

            return products;
        }
    }
}
