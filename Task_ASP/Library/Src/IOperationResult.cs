namespace Task_ASP.Library
{
    public interface IOperationResult
    {
        bool IsSuccessfull { get; }
        string SuccessMessages { get; }
        string WarningMessages { get; }
        string ErrorMessages { get; }
    }
}