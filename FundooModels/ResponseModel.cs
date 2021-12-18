using System;
using System.Collections.Generic;
using System.Text;

namespace FundooModels
{
    /// <summary>
    /// class that stores the response details
    /// </summary>
    /// <typeparam name="T">different type of data</typeparam>
    public class ResponseModel<T>
    {
        /// <summary>
        /// gets or sets a value according to the status as true or false
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// gets or sets the message as string
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// gets or sets a data as given type
        /// data is optional whenever needed data is returned
        /// </summary>
        public T Data { get; set; }
    }
}
