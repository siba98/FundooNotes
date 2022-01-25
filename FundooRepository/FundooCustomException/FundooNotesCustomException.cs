using System;
using System.Net;

namespace FundooRepository.FundooCustomException
{
    public class FundooNotesCustomException : Exception
    {
        public FundooNotesCustomException(string message) : base(message)
        {
        }
    }
}
