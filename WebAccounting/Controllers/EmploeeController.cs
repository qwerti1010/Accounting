using DBLibrary.Entities;
using DBLibrary.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using WebAccounting.Extensions;

namespace WebAccounting.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _service;
    private static int _pageNumber;

    public EmployeeController(EmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("GetAll")]
    public ActionResult<IList<EmployeeDTO>> GetEmployees()
    {
        var employees = _service.GetEmployees(10, 0);
        if (employees.Count == 0) return NotFound(new List<EmployeeDTO>());

        var result = employees.ToDto();
        return Ok(result);

    }

    [HttpGet]
    [Route("GetByID/{id}"), FeatureEnabled(IsEnabled = false)]
    public ActionResult<EmployeeDTO> GetByID(uint id)
    {
        var employee = _service.GetByID(id);
        if (employee == null) return NotFound();
        var result = employee.ToDto();
        return Ok(result);       
    }

    [HttpPost]
    [Route("Create")]
    //[Authorize(Roles = "Admin")]
    public ActionResult<string> Create([FromBody]EmpCreateDTO request)
    {
        var employee = request.ToEmp();
        if (employee == null) return BadRequest("Неправильно указана роль");

        var result = _service.Registration(employee);
        if (!result.IsSuccess) return BadRequest(result.Message);

        return Ok(result.Message);
    }

    [HttpPut]
    [Route("ChangeEmployee")]
    //[Authorize(Roles = "Admin, Moderator")]
    public ActionResult<string> Update([FromBody] EmployeeDTO request)
    {
        var employee = request.ToEmp();
        if (employee == null) return BadRequest("Неправильно указана роль");

        var result = _service.Update(employee);
        if (!result.IsSuccess) return BadRequest(result.Message);

        return Ok(result.Message);
    }    

    [HttpDelete]
    [Route("Delete")]
    //[Authorize(Roles ="Admin")]
    public ActionResult<string> Delete(uint id)
    {
        var result = _service.Delete(id);
        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }
        return Ok(result.Message);
    }

    [HttpGet]
    [Route("Next")]
    public ActionResult<IList<EmployeeDTO>> GetNext()
    {
        IList<Employee> employees;
        var count = _service.Count();
        if (_pageNumber * 10 > count - 10)
        {
            employees = _service.GetEmployees(10, _pageNumber * 10);
        }
        else
        {
            employees = _service.GetEmployees(10, ++_pageNumber * 10);
        }
        var result = employees.ToDto();
        return Ok(result);        
    }

    [HttpGet]
    [Route("Previous")]
    public ActionResult<IList<EmployeeDTO>> GetPrevious()
    {
        IList<Employee> employees;
        if (_pageNumber == 0)
        {
            employees = _service.GetEmployees(10, 0);
        }
        else
        {
            employees = _service.GetEmployees(10, --_pageNumber * 10);
        }
        var result = employees.ToDto();
        return Ok(result);
    }
}
