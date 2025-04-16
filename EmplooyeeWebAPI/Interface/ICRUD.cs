using EmplooyeeWebAPI.Models;

namespace EmplooyeeWebAPI.Interface
{
    public interface ICRUD<T>
    {
        Task<IEnumerable<T>> GetAll();
       
        Task<ResponseBase> Delete(string id);
        Task<T> GetById(string Employeeid);
    }
}
