using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System;

namespace ProgrammersBlog.Shared.Utilities.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; } // RulstStatus.Success  Yada ResultStatus.Error olarak kullanılır . 
        public string Message { get; } // Get  Göndeer Set değiştir .
        public Exception Exception { get; }

    }
}
