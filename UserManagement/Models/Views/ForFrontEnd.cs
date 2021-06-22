using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Models.Views
{
    public class ForFrontEnd<T>
    {
        public T data { get; set; }
        public bool state { get; set; }
        public string message { get; set; }
        public Exception exception
        {
            #if RELEASE
                private get;
            #else
                get;
            #endif
                set;
        }

        public ForFrontEnd(T data, bool state, string message, Exception exception)
        {
            this.data = data;
            this.state = state;
            this.message = message;
            this.exception = exception;
        }

        public static ForFrontEnd<T> True(T data)
        {
            return new ForFrontEnd<T>(data, true, "", null);
        }

        public static ForFrontEnd<T> False(T data, string message, Exception e)
        {
            return new ForFrontEnd<T>(data, false, message, e);
        }
    }
}
