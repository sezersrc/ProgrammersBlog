using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System;

namespace ProgrammersBlog.Shared.Utilities.Results.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(ResultStatus resultStatus, T data)
        {
            // constructor 
            ResultStatus = resultStatus;
            Data = data;

        }

        public DataResult(ResultStatus resultStatus, string message, T data)
        {

            ResultStatus = resultStatus;
            Message = message;
            Data = data;

        }

        public DataResult(ResultStatus resultStatus, string message, T data, Exception exception)
        {

            ResultStatus = resultStatus;
            Message = message;
            Data = data;
            Exception = exception;

        }
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
        public T Data { get; }
    }
}
