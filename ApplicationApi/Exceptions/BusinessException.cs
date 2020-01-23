using System;

namespace ApplicationApi.Exceptions
{
    public class BusinessException: Exception
    {
        public string  CodeErreur { get; set; }

        public BusinessException(string code,string message):base(message)
        {
            CodeErreur=code;
        }
    }
}