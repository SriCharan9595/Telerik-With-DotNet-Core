using Telerik.DataSource;

namespace KendoWebApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfJoining { get; set; }
        public int Experience { get; set; }
        public string Country { get; set; }
    }

    public class DataEnvelope<T>
    {
        public List<AggregateFunctionsGroup> GroupedData { get; set; }
        public List<T> CurrentPageData { get; set; }
        public int TotalItemCount { get; set; }

        public static implicit operator DataEnvelope<T>(DataEnvelope<Employee> v)
        {
            throw new NotImplementedException();
        }
    }
}
