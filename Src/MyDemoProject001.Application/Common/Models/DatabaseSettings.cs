using MyDemoProject001.Application.Common.Interfaces;

namespace MyDemoProject001.Application.Common.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
