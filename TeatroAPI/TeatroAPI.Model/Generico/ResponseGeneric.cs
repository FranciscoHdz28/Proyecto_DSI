using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeatroAPI.Model.Generico
{
    public class ResponseGeneric<T> : Response
    {
        public T? Response { get; set; }

        public ResponseGeneric(T returnObject)
        {
            Response = returnObject;
            base.CurrentException = null;
        }

        public ResponseGeneric(Exception currentException) : base(currentException)
        {
            Response = default(T);
        }

        public ResponseGeneric(string currentExceptionMessage) : base(currentExceptionMessage)
        {
            Response = default(T);
        }

        public ResponseGeneric()
        {
        }
    }

}
