using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Application.Contract.ArticleApp
{
    public class CreateArticle
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessages.MaxLenght)]
        public string Title { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(2000, ErrorMessage = ValidationMessages.MaxLenght)]
        public string ShortDescription { get; set; }


        public string Description { get; set; }


        [MaxFileSize(1 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        [FileExtentionLimitation(new string[] { ".png", ".jpg", ".jpeg" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        public IFormFile? Picture { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessages.MaxLenght)]
        public string PictureAlt { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessages.MaxLenght)]
        public string PictureTitle { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PublishDate { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(600, ErrorMessage = ValidationMessages.MaxLenght)]
        public string Slug { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessages.MaxLenght)]
        public string Keywords { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(2000, ErrorMessage = ValidationMessages.MaxLenght)]
        public string MetaDescription { get; set; }


        [MaxLength(1000, ErrorMessage = ValidationMessages.MaxLenght)]
        public string? CanonicalAddress { get; set; }


        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public long CategoryId { get; set; }
    }
}
