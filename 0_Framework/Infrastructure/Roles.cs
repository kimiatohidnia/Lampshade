using System.Linq.Expressions;

namespace _0_Framework.Infrastructure
{
    public static class Roles
    {
        public const string Admin = "1";
        public const string SystemUser = "2";
        public const string ContentUploader = "3";

        public static string GetRoleById(long id)
        {
            switch (id)
            {
                case 1:
                    return "مدیر سیستم";

                case 3:
                    return "محتواگذار";

                default:
                    return "";
            }
        }
    }
}
