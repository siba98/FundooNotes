using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FundooRepository
{
    public class FundooNotesCustomException : Exception
    { 
        public HttpStatusCode StatusCode { get; private set; }

        public FundooNotesCustomException(HttpStatusCode httpStatusCode, string message) : base(message)
        {
            StatusCode = httpStatusCode;
        }
    }
}
