namespace Task_ASP.Library
{
    public enum MessageType { mtSuccess, mtWarning, mtError }

    public class OperationResult : IOperationResult
    {

        public OperationResult(string message, MessageType messageType)
        {
            IsSuccessfull = (messageType == MessageType.mtSuccess);

            switch (messageType)
            {
                case MessageType.mtSuccess:
                    SuccessMessages = message;
                    break;
                case MessageType.mtWarning:
                    WarningMessages = message;
                    break;
                case MessageType.mtError:
                    ErrorMessages = message;
                    break;
            }
        }

        public bool IsSuccessfull { get; private set; }
        public string SuccessMessages { get; private set; }
        public string ErrorMessages { get; private set; }
        public string WarningMessages { get; private set; }
    }

    public class SuccessfullOperation : OperationResult
    {
        public SuccessfullOperation(string message) : base(message, MessageType.mtSuccess) { }
    };

    public class OperationWithWarnings : OperationResult
    {
        public OperationWithWarnings(string message) : base(message, MessageType.mtWarning) { }
    };

    public class OperationWithErrors : OperationResult
    {
        public OperationWithErrors(string message) : base(message, MessageType.mtError) { }
    };

}