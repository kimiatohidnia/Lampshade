using _0_Framework.Application;
using BlogManagement.Application.Contract.ArticleApp;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _repository;
        private readonly IArticleQuery _query;
        private readonly IFileUploader _fileUploader;
        private readonly IArticleCategoryRepository _categoryRepository;

        public ArticleApplication(IArticleRepository repository, IArticleQuery query, IFileUploader fileUploader, IArticleCategoryRepository categoryRepository)
        {
            _repository = repository;
            _query = query;
            _fileUploader = fileUploader;
            _categoryRepository = categoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();

            if (_repository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            var categorySlug = _categoryRepository.GetSlugById(command.CategoryId);
            var picturePath = $"{categorySlug}/{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, picturePath);

            var publishDate = command.PublishDate.ToGeorgianDateTime();

            var article = new Article
                (
                    command.Title,
                    command.ShortDescription,
                    command.Description,
                    publishDate,
                    pictureName,
                    command.PictureAlt,
                    command.PictureTitle,
                    slug,
                    command.Keywords,
                    command.MetaDescription,
                    command.CanonicalAddress,
                    command.CategoryId
                );

            _repository.Create(article);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();

            var article = _repository.GetArticleWithCategoryById(command.Id);

            if (article == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            var picturePath = $"{article.Category.Slug}/{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, picturePath);

            var publishDate = command.PublishDate.ToGeorgianDateTime();

            article.Edit
                (
                    command.Title,
                    command.ShortDescription,
                    command.Description,
                    publishDate,
                    pictureName,
                    command.PictureAlt,
                    command.PictureTitle,
                    slug,
                    command.Keywords,
                    command.MetaDescription,
                    command.CanonicalAddress,
                    command.CategoryId
                );

            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditArticle GetDetails(long id)
        {
            return _query.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _query.Search(searchModel);
        }
    }
}
