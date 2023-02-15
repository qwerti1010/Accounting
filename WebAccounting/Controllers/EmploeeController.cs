using DBLibrary.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using WebAccounting.Extensions;

namespace WebAccounting.Controllers;

[ApiController]
[Authorize]
public class EmploeeController : ControllerBase
{
    private readonly EmployeeService _service;
    private static int _pageNumber;

    public EmploeeController(EmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("GetAll")]
    public ActionResult<IList<EmployeeDTO>> GetEmployees()
    {
        var x = HttpContext.Request.Headers.Authorization[0];
        var employees = _service.GetEmployees(10, 0);
        if (employees.Count == 0) return NotFound();

        var result = employees.ToDto();
        return Ok(result);

    }

    [HttpGet]
    [Route("GetByID"), FeatureEnabled(IsEnabled = false)]
    public ActionResult<EmployeeDTO> GetByID(uint id)
    {
        var employee = _service.GetByID(id);
        if (employee == null) return NotFound();
        var result = employee.ToDto();
        return Ok(result);       
    }

    [HttpPost]
    [Route("CreateEmployee")]
    [Authorize(Roles = "Admin")]
    //нужна отдельная форма с ролью
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
    [Authorize(Roles = "Admin, Moderator")]
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
    [Authorize(Roles ="Admin")]
    //сделать сервис.делит возвращаемым эмплойереспонс
    public ActionResult Delete(uint id)
    {
        _service.Delete(id);
        return Ok();
    }

    [HttpGet]
    [Route("GetNext")]
    public ActionResult<IList<EmployeeDTO>> GetNext()
    {
        var count = _service.Count();
        if (_pageNumber * 10 > count - 10) return NoContent();

        var employees = _service.GetEmployees(10, ++_pageNumber * 10);
        var result = employees.ToDto();
        return Ok(result);        
    }

    [HttpGet]
    [Route("Previous")]
    public ActionResult<IList<EmployeeDTO>> GetPrevious()
    {
        if (_pageNumber == 0) return NoContent();

        var employees = _service.GetEmployees(10, --_pageNumber * 10);
        var result = employees.ToDto();
        return Ok(result);
    }
}
