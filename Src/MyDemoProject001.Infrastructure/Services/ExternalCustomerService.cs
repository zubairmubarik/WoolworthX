using MyDemoProject001.Application.Common.Interfaces;
using MyDemoProject001.Application.Common.Models;
using System;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using Newtonsoft.Json;

using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace MyDemoProject001.Infrastructure.Services
{
    public class ExternalCustomerService : IExternalService<CustomerDto>
    {


        #region Data Members
        private readonly IHttpClientFactory _clientFactory;
        protected readonly IServiceEndPoints _options;
        #endregion

        #region Constructor with Dependency Injection

        public ExternalCustomerService(IHttpClientFactory clientFactory, IOptions<IServiceEndPoints> options)
        {
            _clientFactory = clientFactory;
            _options = options.Value;
        }

        #endregion

        #region Public Service Actions      
        public Task<CustomerDto> DeleteAsync(string id, CustomerDto item)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDto> GetAsync()
        {
            var responseString = await GetTransactionDataWithHeaders();
            return JsonConvert.DeserializeObject<CustomerDto>(responseString);
        }

        public Task<CustomerDto> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDto> PostAsync(CustomerDto item)
        {
            string value = "";
            DateTime dateTime = DateTime.Now;
            var responseString = await PostTransactionDataWithHeaders(value, dateTime);
            return JsonConvert.DeserializeObject<CustomerDto>(responseString);
        }

        public Task<CustomerDto> PutAsync(string id, CustomerDto item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        private async Task<string> GetTransactionDataWithHeaders()
        {
            //var paymentEngineData = new PaymentEngineData()
            //{
            //    FromLastDaysForMonthData = _options.fromLastDaysForMonthData,
            //    ToLastDaysForMonthData = _options.toLastDaysForMonthData
            //};
            var paymentEngineData = "";
            string value = JsonConvert.SerializeObject(paymentEngineData);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_options.ClientServiceEndpoint}/dpadmin/v1/apiDashboardMonthlyTransactions");

            //request.Headers.Authorization = new AuthenticationHeaderValue(
            //                                 "Basic",
            //                                 Convert.ToBase64String(
            //                                     System.Text.ASCIIEncoding.ASCII.GetBytes(
            //                                         string.Format("{0}:{1}", "F7DD1315-4550-4388-87C4-34728C215BF8", ""))));

            request.Headers.Authorization = new AuthenticationHeaderValue(
                                           "Basic",
                                           Convert.ToBase64String(
                                               System.Text.ASCIIEncoding.ASCII.GetBytes(
                                                   string.Format("{0}:{1}", _options.ClientServiceEndpointToken, ""))));

            request.Content = new StringContent(value, Encoding.UTF8, "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"StatusCode: {response.StatusCode}";
            }
        }

     

        private async Task<string> PostTransactionDataWithHeaders(string value, DateTime dateTime)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_options.ClientServiceEndpoint}/dpadmin/v1/apiDashboardRealTimeTransactions");

            //request.Headers.Authorization = new AuthenticationHeaderValue(
            //                                 "Basic",
            //                                 Convert.ToBase64String(
            //                                     System.Text.ASCIIEncoding.ASCII.GetBytes(
            //                                         string.Format("{0}:{1}", "F7DD1315-4550-4388-87C4-34728C215BF8", ""))));

            //request.Headers.Authorization = new AuthenticationHeaderValue(
            //                               "Basic",
            //                               Convert.ToBase64String(
            //                                   System.Text.ASCIIEncoding.ASCII.GetBytes(
            //                                       string.Format("{0}:{1}", _options.dpAdminIssuerAccountDev, _options.dpAdminIssuerPasswordDev))));

            request.Headers.Authorization = new AuthenticationHeaderValue(
                                         "Basic",
                                         Convert.ToBase64String(
                                             System.Text.ASCIIEncoding.ASCII.GetBytes(
                                                 string.Format("{0}:{1}", _options.ClientServiceEndpointToken, ""))));

            request.Content = new StringContent(JsonConvert.SerializeObject(new { lastValue = value, lastDateTime = dateTime }), Encoding.UTF8, "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"StatusCode: {response.StatusCode}";
            }
        }


      

        #endregion
    }
}
