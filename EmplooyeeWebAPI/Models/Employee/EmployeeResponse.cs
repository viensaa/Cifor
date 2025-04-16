using EmployeeAPI.DomainObject.Employee;

namespace EmplooyeeWebAPI.Models.Employee
{
    public class EmployeeResponse : ResponseBase
    {
        public IEnumerable<EmployeeData> datas { get; set; }
    }
}
