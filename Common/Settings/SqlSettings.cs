using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Settings
{
    public class SqlSettings
    {

        public string Server { get; set; }

        public string Catalog { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public SqlSettings() { }
        public SqlSettings(string server, string catalog, string user, string password)
        {
            Server = server;
            Catalog = catalog;
            User = user;
            Password = password;
        }
    }
}