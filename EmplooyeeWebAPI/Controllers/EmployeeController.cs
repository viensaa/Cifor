using AutoMapper;
using EmplooyeeWebAPI.BusinessFacade;
using EmplooyeeWebAPI.Interface;
using EmplooyeeWebAPI.Models;
using EmplooyeeWebAPI.Models.Employee;
using EmployeeAPI.DomainObject.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmplooyeeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployee employee,IMapper mapper) 
        {
            _mapper = mapper;
            _employee = employee;
        }



        [HttpGet]
        public async Task<IEnumerable<EmployeeData>> GetData()
        {

            var results = await _employee.GetAll();
            return results;
        }

        [HttpGet("{EmployeeId}")]
        public  async Task<IActionResult> GetDataByID(string EmployeeId)
        {

            var results = await _employee.GetById(EmployeeId);
            if (results == null) 
            {
                return NotFound($"Employee dengan EmployeeID {EmployeeId} Tidak Ditemukan");
            }
            return Ok(results);
        }
        [HttpGet("(Search)")]
        public async Task<IActionResult> GetDataByID([FromQuery]SearchCustom request)
        {

            var results = await _employee.GetAllByCustomSearch(request);
            if (results == null || results.Count() == 0)
            {
                return NotFound($"Employee  Tidak Ditemukan");
            }
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InsertEmployeeRequest request)
        {

            try
            {
                var results = await _employee.Insert(request);
                if (!results.IsSuccess)
                {
                    return BadRequest(results.Message);
                }
                return Ok(results);
            }
            catch (Exception)
            {

                throw;
            }
            
        
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(string employeeId) 
        {
            ResponseBase response = new ResponseBase();
            response = await _employee.Delete(employeeId);
            if (!response.IsSuccess) 
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }


    }
}
