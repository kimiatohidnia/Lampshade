using _0_Framework.Application;
using ShopManagement.Application.Contract.SlideApp;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application.SlideApp
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _repository;
        private readonly ISlideQuery _query;
        private readonly IFileUploader _fileUploader;

        public SlideApplication(ISlideRepository repository, ISlideQuery query, IFileUploader fileUploader)
        {
            _repository = repository;
            _query = query;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();

            var pictureName = _fileUploader.Upload(command.Picture, "Slides");

            var slide = new Slide
            (
                pictureName,
                command.PictureAlt,
                command.PictureTitle,
                command.Heading,
                command.Title,
                command.Text,
                command.BtnText,
                command.Link
            );

            _repository.Create(slide);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();
            var sldie = _repository.Get(command.Id);

            if (sldie == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            var pictureName = _fileUploader.Upload(command.Picture, "Slides");

            sldie.Edit(pictureName, command.PictureAlt, command.PictureTitle,
                command.Heading, command.Title, command.Text, command.BtnText, command.Link);

            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var slide = _repository.Get(id);

            if (slide == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slide.Remove();
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var slide = _repository.Get(id);

            if (slide == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slide.Restore();
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditSlide? GetDetails(long id)
        {
            return _query.GetDetails(id);
        }

        public List<SlideViewModel> GetSlides()
        {
            return _query.GetSlides();
        }
    }
}
