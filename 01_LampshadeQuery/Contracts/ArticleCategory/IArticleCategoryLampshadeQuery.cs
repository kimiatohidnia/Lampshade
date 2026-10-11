namespace _01_LampshadeQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryLampshadeQuery
    {
        ArticleCategoryQueryModel GetArticleCategory(string slug);
        List<ArticleCategoryQueryModel> GetArticleCategories();
    }
}
