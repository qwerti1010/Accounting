using DBLibrary;
using DBLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace WebAccounting.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmploeeController : ControllerBase
{
    private readonly EmployeeService _service;
    
    public EmploeeController()
    {
        var context = new DbConnect();
        _service = new EmployeeService(context);
    }

    [HttpGet]
    [Route("GetAll")]
    public ActionResult<IList<Employee>> GetEmployees()
    {
        var result = _service.GetEmployees(10, 0);
        if (result.Count == 0) return NotFound();

        return Ok(result);
    }

    [HttpGet]
    [Route("GetByID")]
    public ActionResult<Employee> GetByID(uint id)
    {
        var result = _service.GetByID(id);
        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpPost]
    [Route("CreateEmployee")]
    public ActionResult Registration(Employee employee)
    {
        var result = _service.Registration(employee);
        if (!result.IsSuccess) return BadRequest(result.Message);

        return Ok(result.Message);
    }

    [HttpPut]
    [Route("ChangeEmployee")]
    public ActionResult Update(Employee employee)
    {
        var result = _service.Update(employee);
        if (!result.IsSuccess) return BadRequest(result.Message);
        return Ok(result.Message);
    }

    [HttpGet]
    [Route("Login")]
    public ActionResult<Employee> Login(string login, string password)
    {
        var result = _service.Login(login, password);
        if (!result.IsSuccess) return BadRequest(result.Message);

        return Ok(result.Employee);
    }

    [HttpDelete]
    [Route("Delete")]
    //сделать сервис.делит возвращаемым эмплойереспонс
    public ActionResult Delete(uint id)
    {
        _service.Delete(id);
        return Ok();
    }
}
