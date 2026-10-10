using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Application.Contract.ArticleCategoryApp
{
    public class CreateArticleCategory
    {
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string Name { get; set; }


        [MaxFileSizeAttribute(1 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        [FileExtentionLimitation(new string[] { ".png", ".jpg", ".jpeg" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        public IFormFile? Picture { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public int ShowOrder { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get; set; }


        public string? CanonicalAddress { get; set; }
    }
}
