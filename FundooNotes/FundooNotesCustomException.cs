using System;

namespace FundooNotes
{
    public class FundooNotesCustomException : Exception
    {
        public enum ExceptionType
        {
            EMPTY_PARAMETER
        }
        private readonly ExceptionType type;
        public FundooNotesCustomException(ExceptionType Type, String message) : base(message)
        {
            this.type = Type;
        }
    }
}
