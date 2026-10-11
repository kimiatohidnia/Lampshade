using _0_Framework.Application;
using BlogManagement.Application.Contract.ArticleCategoryApp;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _repository;
        private readonly IArticleCategoryQuery _query;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository repository, IArticleCategoryQuery query, IFileUploader fileUploader)
        {
            _repository = repository;
            _query = query;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();

            if (_repository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            var pictureName = _fileUploader.Upload(command.Picture, slug);

            var articleCategory = new ArticleCategory
                (
                    command.Name,
                    command.Description,
                    command.ShowOrder,
                    pictureName,
                    command.PictureAlt,
                    command.PictureTitle,
                    slug,
                    command.Keywords,
                    command.MetaDescription,
                    command.CanonicalAddress
                );

            _repository.Create(articleCategory);
            _repository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();

            var articleCategory = _repository.Get(command.Id);

            if (articleCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            var pictureName = _fileUploader.Upload(command.Picture, slug);

            articleCategory.Edit
                (
                    command.Name,
                    command.Description,
                    command.ShowOrder,
                    pictureName,
                    command.PictureAlt,
                    command.PictureTitle,
                    slug,
                    command.Keywords,
                    command.MetaDescription,
                    command.CanonicalAddress
                );

            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ArticleCategoryViewModel> GetCategories()
        {
            return _query.GetCategories();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _query.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _query.Search(searchModel);
        }
    }
}
