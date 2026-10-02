using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contract.SlideApp
{
    public class CreateSlide
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Picture { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Heading { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Title { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Text { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string BtnText { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Link { get; set; }
    }
}
