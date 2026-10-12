using CommentManagement.Application.CommentApp;
using CommentManagement.Application.Contract.CommentApp;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Configuration
{
    public class CommentManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ICommentApplication, CommentApplication>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICommentQuery, CommentQuery>();

            services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
