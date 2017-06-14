using System;
namespace TodoApi.Environments
{
    public class Configuration
    {
        public LoggingConfiguration Logging
        {
            get;
            set;
        }

        public DatabaseConfiguration Database
        {
            get;
            set;
        }
    }
}
