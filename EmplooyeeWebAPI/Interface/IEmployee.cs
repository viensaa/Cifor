using EmplooyeeWebAPI.Models;
using EmplooyeeWebAPI.Models.Employee;
using EmployeeAPI.DomainObject;
using EmployeeAPI.DomainObject.Employee;


namespace EmplooyeeWebAPI.Interface
{
    public interface IEmployee :ICRUD<EmployeeData>
    {
        Task<IEnumerable<EmployeeData>> GetAllByCustomSearch(SearchCustom request);
        Task<ResponseBase> Insert(InsertEmployeeRequest reqeust);
    }
}
