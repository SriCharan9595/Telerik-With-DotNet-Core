using KendoWebApi.Models;
using KendoWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Telerik.DataSource;

namespace KendoWebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class KendoWebApiController : ControllerBase
    {
        private readonly IDataSourceService _dataSourceService;

        public KendoWebApiController(IDataSourceService dataSourceService)
        {
            _dataSourceService = dataSourceService;
        }

        [HttpGet("GetEmployees")]
        [Produces("application/json")]
        public async Task<ActionResult<Employee>> GetEmployees()
        {
            try
            {
                var result = await _dataSourceService.GetResponseFromHttpRequest();
                return result == null || !result.Any() ? NotFound("No response found from this request") : Ok(result);
            }
            catch (ApplicationException exception)
            {
                throw new ApplicationException($"method execution failed with Business Exception - Message: {exception.Message}");
            }
            catch (AggregateException exception)
            {
                throw new AggregateException($"method execution failed with Aggregate Exception - Message: {exception.Message}");
            }
            catch (Exception exception)
            {
                throw new Exception($"method execution failed with System Exception - Message: {exception.Message}");
            }
        }

        [HttpPost("GetFilteredEmployees")]
        [Produces("application/json")]
        public async Task<ActionResult<DataEnvelope<Employee>>> GetFilteredEmployees([FromBody] DataSourceRequest dataSourceRequest)
        {
            try
            {
                var apiResponse = await _dataSourceService.GetResponseFromHttpRequest();

                if (apiResponse == null || !apiResponse.Any())
                {
                    return NotFound("No response found from this request");
                }

                var result = await _dataSourceService.GetResponseFromDataOperations(dataSourceRequest, apiResponse);
                return Ok(result);                
            }
            catch (ApplicationException exception)
            {
                throw new ApplicationException($"method execution failed with Business Exception - Message: {exception.Message}");
            }
            catch (AggregateException exception)
            {
                throw new AggregateException($"method execution failed with Aggregate Exception - Message: {exception.Message}");
            }
            catch (Exception exception)
            {
                throw new Exception($"method execution failed with System Exception - Message: {exception.Message}");
            }
        }

        [HttpGet("GetHealth")]
        [Produces("application/json")]
        public string GetHealth()
        {
            return "Hello SK...";
        }
    }   
}
