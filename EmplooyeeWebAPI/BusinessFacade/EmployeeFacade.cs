using AutoMapper;
using Azure;
using Azure.Core;
using EmplooyeeWebAPI.Interface;
using EmplooyeeWebAPI.Models;
using EmplooyeeWebAPI.Models.Employee;
using EmployeeAPI.DomainObject;
using EmployeeAPI.DomainObject.Employee;
using Microsoft.EntityFrameworkCore;

namespace EmplooyeeWebAPI.BusinessFacade
{
    public class EmployeeFacade : IEmployee
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EmployeeFacade(DataContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseBase> Delete(string id)
        {
            ResponseBase rs = new ResponseBase();
            try
            {
                var findData = await _context.Employees.SingleOrDefaultAsync(x => x.EmployeeId == id);
                if (findData == null)
                {
                    rs.IsSuccess = false;
                    rs.Message = $"Employee dengan ID {id} Tidak DiTemukan ";
                    return rs;
                }
                _context.Employees.Remove(findData);
                await _context.SaveChangesAsync();
                rs.IsSuccess = true;
                rs.Message = "Delete Success";
            }
            catch (Exception ex)
            {
                rs.IsSuccess = false;
                rs.Message = "Delete Data Failed";
            }
            return rs;
        }

        public async Task<IEnumerable<EmployeeData>> GetAll()
        {
            List<EmployeeData> results =  new List<EmployeeData>();
            var datas = await _context.Employees.OrderBy(x => x.EmployeeId).ToListAsync();
            

            foreach (var data in datas) 
            {
                EmployeeData newData = new EmployeeData();
                newData.EmployeeId = data.EmployeeId;
                newData.Address = data.Address;
                newData.Email = data.Email;
                newData.Departement = data.Departement;
                newData.Name = data.Name;
                results.Add(newData);
                 }
            return results;
        }

        public async Task<IEnumerable<EmployeeData>> GetAllByCustomSearch(SearchCustom request)
        {
            try
            {
                var query = _context.Employees.OrderBy(x=>x.EmployeeId).AsQueryable();
                if (!string.IsNullOrEmpty(request.Name)) 
                {
                    query = query.Where(x=>x.Name == request.Name);
                }
                if (!string.IsNullOrEmpty(request.Departement))
                {
                    query = query.Where(x => x.Departement == request.Departement);
                }

                var datas = await query.ToListAsync();
                List<EmployeeData> results = new List<EmployeeData>();
                foreach (var data in datas)
                {
                    EmployeeData newData = new EmployeeData();
                    newData.EmployeeId = data.EmployeeId;
                    newData.Address = data.Address;
                    newData.Email = data.Email;
                    newData.Departement = data.Departement;
                    newData.Name = data.Name;
                    results.Add(newData);
                }
                return results;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeData> GetById(string Employeeid)
        {
            var result = await _context.Employees.FirstOrDefaultAsync(x=>x.EmployeeId == Employeeid);
            if (result == null) 
            {
                return null;
            }
            EmployeeData data = new EmployeeData();
            data.EmployeeId = result.EmployeeId;
            data.Address = result.Address;
            data.Email = result.Email;
            data.Departement= result.Departement;
            data.Name = result.Name;
            return data;
        }

        public async Task<ResponseBase> Insert(InsertEmployeeRequest reqeust)
        {
            ResponseBase response = new ();
            
            try
            {
                #region ValidationData
                if (string.IsNullOrEmpty(reqeust.EmployeeId) || string.IsNullOrEmpty(reqeust.Name) || string.IsNullOrEmpty(reqeust.Departement) || string.IsNullOrEmpty(reqeust.Address) || string.IsNullOrEmpty(reqeust.Email)) 
                {
                    response.IsSuccess = false;
                    response.Message = ("Reqeust Not Valid");
                    return response;
                }

                bool dupicateCheck = await _context.Employees.AnyAsync(x => x.EmployeeId == reqeust.EmployeeId);
                if (dupicateCheck) 
                {
                    response.IsSuccess = false;
                    response.Message = ($"EmployeeID {reqeust.EmployeeId} Sudah Terdaftar");
                    return response;
                }
                #endregion

                EmployeeDomainObject dataInput = new EmployeeDomainObject();
                dataInput.Address = reqeust.Address;
                dataInput.Email = reqeust.Email;
                dataInput.Departement = reqeust.Departement;
                dataInput.Name = reqeust.Name;
                dataInput.EmployeeId = reqeust.EmployeeId;

                _context.Employees.Add(dataInput);
                await _context.SaveChangesAsync();
                response.IsSuccess = true;
                response.Message = "Insert Success";
                
            }
            catch (Exception ex)
            {

                response.IsSuccess=false;
                response.Message = "Insert Data Failed";
            }
            return response;
        }

        public async Task<ResponseBase> Update(InsertEmployeeRequest reqeust)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                #region ValidationData
                if (string.IsNullOrEmpty(reqeust.EmployeeId) || string.IsNullOrEmpty(reqeust.Name) || string.IsNullOrEmpty(reqeust.Departement) || string.IsNullOrEmpty(reqeust.Address) || string.IsNullOrEmpty(reqeust.Email))
                {
                    response.IsSuccess = false;
                    response.Message = ("Reqeust Not Valid");
                    return response;
                }

                var findData = await _context.Employees.SingleOrDefaultAsync(x => x.EmployeeId == reqeust.EmployeeId);
                if (findData == null )
                {
                    response.IsSuccess = false;
                    response.Message = ($"EmployeeID {reqeust.EmployeeId} Tidak Ditemukan");
                    return response;
                }
                #endregion


                findData.Address = reqeust.Address;
                findData.Email = reqeust.Email;
                findData.Departement = reqeust.Departement;
                findData.Name = reqeust.Name;
                findData.EmployeeId = reqeust.EmployeeId;

            
                await _context.SaveChangesAsync();
                response.IsSuccess = true;
                response.Message = "Update Data Success";
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.Message = "Update Data Failed";
            }
            return response;
        }
    }
}
