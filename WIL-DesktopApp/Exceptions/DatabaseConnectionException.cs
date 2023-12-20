using System;

namespace WIL_DesktopApp.Exceptions
{
    public class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException() { }
        public override string ToString()
        {
            return "Database connection failed " + base.ToString();
        }
    }
}
