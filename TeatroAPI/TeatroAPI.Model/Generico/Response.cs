using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeatroAPI.Model.Generico
{
    public class Response
    {
        public ResponseStatus Status { get; set; }
        public string? TraceInformation { get; set; }
        public string? CurrentException { get; set; }

        public Response()
        {
            Status = ResponseStatus.Success;
        }

        public Response(Exception currentException)
        {
            CurrentException = currentException.ToString();
            Status = ResponseStatus.Failed;
        }

        public Response(string currentExceptionMessage)
        {
            CurrentException = currentExceptionMessage;
            Status = ResponseStatus.Failed;
        }
    }

    public enum ResponseStatus
    {
        Success,
        Failed
    }

}
