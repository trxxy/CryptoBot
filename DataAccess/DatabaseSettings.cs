using System.Collections.Generic;

namespace DataAccess
{
    public class DatabaseSettings
    {
        public Dictionary<string,string> ConnectionStrings { get; set; }
        public DatabaseType CurrentDatabaseType { get; set; }
    }
}
