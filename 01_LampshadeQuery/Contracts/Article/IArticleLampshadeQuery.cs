namespace _01_LampshadeQuery.Contracts.Article
{
    public interface IArticleLampshadeQuery
    {
        List<ArticleQueryModel> LatestArticles();
        ArticleQueryModel GetArticleDetails(string slug);
    }
}
