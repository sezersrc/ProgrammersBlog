using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System;

namespace ProgrammersBlog.Shared.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public Result(ResultStatus resultStatus)
        {
            ResultStatus = resultStatus;

        }
        public Result(ResultStatus resultStatus, string message)
        {
            ResultStatus = resultStatus;
            Message = message;

        }

        public Result(ResultStatus resultStatus, string message, Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Exception = exception;

        }

        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }

        // Örnek ,; New Result (ResultStatus.Error,"İşlem başarısız oldu.",exeption)
        // Örnek 2 ,; New Result (ResultStatus.Error,exeption.message,exeption)
    }
}
