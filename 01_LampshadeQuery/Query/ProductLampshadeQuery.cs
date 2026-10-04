using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

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
            }).OrderByDescending(x => x.Id).Take(6).ToList();

            
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
    }
}
