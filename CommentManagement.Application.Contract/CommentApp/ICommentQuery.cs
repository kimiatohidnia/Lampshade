namespace CommentManagement.Application.Contract.CommentApp
{
    public interface ICommentQuery
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
