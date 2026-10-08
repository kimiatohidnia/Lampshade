using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contract.ProductApp;

namespace ShopManagement.Application.Contract.ProductPictureApp
{
    public class CreateProductPicture
    {
        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }


        [MaxFileSize(1 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        [FileExtentionLimitation(new string[] { ".png", "jpg", "jpeg" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        public IFormFile? Picture { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }


        public List<ProductViewModel> Products { get; set; }
    }
}
