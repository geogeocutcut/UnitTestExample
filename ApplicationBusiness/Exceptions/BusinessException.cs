using System;

namespace ApplicationBusiness.Exceptions
{
    public enum BusinessExceptionCode
    {
        NO_VALIDE_SOCIETE,
        NO_VALIDE_SIRET,
        NO_VALIDE_NOM
    }
    public class BusinessException : Exception
    {
        public BusinessExceptionCode CodeErreur { get; set; }

        public BusinessException(BusinessExceptionCode code, string message) : base(message)
        {
            CodeErreur = code;
        }
    }
}