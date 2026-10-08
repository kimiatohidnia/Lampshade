using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contract.ProductCategoryApp;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contract.ProductApp
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Code { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string ShortDescription { get; set; }

        public string Description { get; set; }


        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        [FileExtentionLimitation(new string[] { ".png", ".jpg", ".jpeg" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        public IFormFile? Picture { get; set; }


        public string PictureAlt { get; set; }


        public string PictureTitle { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; set; }


        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long CategoryId { get; set; }


        public List<ProductCategoryViewModel> Categories { get; set; }
    }
}
