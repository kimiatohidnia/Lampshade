using BlogManagement.Application.Contract.ArticleCategoryApp;

namespace BlogManagement.Application.Contract.ArticleApp
{
    public class ArticleSearchModel
    {
        public string Title { get; set; }
        public long CategoryId { get; set; }
        public List<ArticleCategoryViewModel> Categories { get; set; }
    }
}
