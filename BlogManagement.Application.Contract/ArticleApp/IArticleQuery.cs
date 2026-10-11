using BlogManagement.Application.Contract.ArticleCategoryApp;

namespace BlogManagement.Application.Contract.ArticleApp
{
    public interface IArticleQuery
    {
        EditArticle? GetDetails(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
