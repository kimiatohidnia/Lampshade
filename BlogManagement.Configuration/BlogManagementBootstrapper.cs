using BlogManagement.Infrastructure.EFCore;
using Microsoft.Extensions.DependencyInjection;
using BlogManagement.Application.Contract.ArticleCategoryApp;
using BlogManagement.Application;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using BlogManagement.Application.Contract.ArticleApp;
using BlogManagement.Domain.ArticleAgg;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using _01_LampshadeQuery.Query;
using _01_LampshadeQuery.Contracts.Article;

namespace ShopManagement.Configuration
{
    public class BlogManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();
            services.AddTransient<IArticleCategoryLampshadeQuery, ArticleCategoryLampshadeQuery>();

            services.AddTransient<IArticleApplication, ArticleApplication>();
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IArticleQuery, ArticleQuery>();
            services.AddTransient<IArticleLampshadeQuery, ArticleLampshadeQuery>();

            services.AddDbContext<BlogContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
