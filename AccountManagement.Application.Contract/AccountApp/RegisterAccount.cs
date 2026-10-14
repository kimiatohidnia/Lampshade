using _0_Framework.Application;
using AccountManagement.Application.Contract.RoleApp;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contract.AccountApp
{
    public class RegisterAccount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Fullname { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Username { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Mobile { get; set; }


        public long RoleId { get; set; }


        [MaxFileSize(1 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        [FileExtentionLimitation(new string[] {".png", ".jpg", ".jpeg"}, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        public IFormFile? ProfilePhoto { get; set; }


        public List<RoleViewModel> Roles { get; set; }
    }
}
