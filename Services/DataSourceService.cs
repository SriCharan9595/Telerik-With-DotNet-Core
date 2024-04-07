using KendoWebApi.Models;
using Newtonsoft.Json;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;

namespace KendoWebApi.Services
{
    public class DataSourceService : IDataSourceService
    {
        private readonly IHttpClientFactory _httpClient;

        public DataSourceService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Employee>> GetResponseFromHttpRequest()
        {
            string apiUrl = "https://mocki.io/v1/eaee4f4e-2d0a-48d3-87d3-1d99190474bd";

            try
            {
                // Make a GET request
                var httpResponse = await _httpClient.CreateClient().GetAsync(apiUrl);

                // Check if the request was successful
                httpResponse.EnsureSuccessStatusCode();

                // Deserialize the response content into List of Employee
                var apiResponse = await httpResponse.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Employee>>(apiResponse);

                return result;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Application Expection occurred - Message: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"System Exception occurred - Message: {ex.Message}");
            }
        }

        public async Task<DataEnvelope<Employee>> GetResponseFromDataOperations(DataSourceRequest dataSourceRequest, List<Employee> employeeResponse)
        {
            try
            {
                DataEnvelope<Employee> dataToReturn;

                var processedData = await employeeResponse.AsQueryable().ToDataSourceResultAsync(dataSourceRequest);
                
                dataToReturn = new DataEnvelope<Employee>
                {
                    CurrentPageData = processedData.Data.Cast<Employee>().ToList(),
                    TotalItemCount = processedData.Total
                };

                return dataToReturn;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Application Expection occurred - Message: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"System Exception occurred - Message: {ex.Message}");
            }
        }
    }
}