namespace AccountManagement.Application.Contract.AccountApp
{
    public interface IAccountQuery
    {
        EditAccount? GetDetails(long id);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
    }
}
