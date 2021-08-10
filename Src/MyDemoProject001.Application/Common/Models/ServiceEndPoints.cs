using MyDemoProject001.Application.Common.Interfaces;

namespace MyDemoProject001.Application.Common.Models
{
    public class ServiceEndPoints : IServiceEndPoints
    {
        public string ClientServiceEndpoint { get; set; }
        public string ClientServiceEndpointToken { get; set; }
        public string ClientServiceUserName { get; set; }
    }
}
