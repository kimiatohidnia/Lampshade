namespace BlogManagement.Application.Contract.ArticleCategoryApp
{
    public interface IArticleCategoryQuery
    {
        EditArticleCategory? GetDetails(long id);
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    }
}
