using _0_Framework.Application;
using AccountManagement.Application.Contract.AccountApp;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountQuery _accountQuery;
        private readonly IAuthHelper _authHelper;

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher,
             IAuthHelper authHelper, IAccountQuery accountQuery, IFileUploader fileUploader)
        {
            _authHelper = authHelper;
            _passwordHasher = passwordHasher;
            _accountRepository = accountRepository;
            _accountQuery = accountQuery;
            _fileUploader = fileUploader;
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();

            var account = _accountRepository.Get(command.Id);

            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);

            var password = _passwordHasher.Hash(command.Password);

            account.ChangePassword(password);

            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();

            if (_accountRepository.Exists(x => x.Username == command.Username || x.Mobile == command.Mobile))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var password = _passwordHasher.Hash(command.Password);

            var pictureName = _fileUploader.Upload(command.ProfilePhoto, "profilePhotos");

            var account = new Account
                (
                    command.Fullname,
                    command.Username,
                    password,
                    command.Mobile,
                    command.RoleId,
                    pictureName
                );

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();

            var account = _accountRepository.Get(command.Id);

            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_accountRepository.Exists(x => (x.Username == command.Username || x.Mobile == command.Mobile) && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var pictureName = _fileUploader.Upload(command.ProfilePhoto, "profilePhotos");

            account.Edit
                (
                    command.Fullname,
                    command.Username,
                    command.Mobile,
                    command.RoleId,
                    pictureName
                );

            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditAccount? GetDetails(long id)
        {
            return _accountQuery.GetDetails(id);
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();

            var account = _accountRepository.GetBy(command.Username);

            if (account == null)
                return operation.Failed(ApplicationMessages.WrongUserPass);

            (bool Verified, bool NeedsUpgrade) result = _passwordHasher.Check(account.Password, command.Password);

            if (!result.Verified)
                return operation.Failed(ApplicationMessages.WrongUserPass);

            var authViewModel = new AuthViewModel
                (
                    account.Id,
                    account.RoleId,
                    account.Fullname,
                    account.Username
                );

            _authHelper.Signin(authViewModel);

            return operation.Succeeded();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountQuery.Search(searchModel);
        }
    }
}
