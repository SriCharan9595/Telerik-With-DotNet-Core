using KendoWebApi.Models;
using Telerik.DataSource;

namespace KendoWebApi.Services
{
    public interface IDataSourceService
    {
        Task<List<Employee>> GetResponseFromHttpRequest();
        Task<DataEnvelope<Employee>> GetResponseFromDataOperations(DataSourceRequest dataSourceRequest, List<Employee> employeesResponse);
    }
}