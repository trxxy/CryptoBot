using System;

namespace DataAccess.Exceptions
{
    public class InvalidAppSettingsKeyException : Exception
    {
        public InvalidAppSettingsKeyException() : base() { }
        public override string Message => "Invalid keyvalue in appsetting.json";
    }
}
