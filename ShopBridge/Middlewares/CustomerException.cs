using System;
using System.Globalization;

namespace ShopBridge.Middlewares
{
    public class CustomerException: Exception
    {
        public CustomerException() : base() { }

        public CustomerException(string message) : base(message) { }

        public CustomerException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
