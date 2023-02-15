using DBLibrary.Entities;
using DBLibrary.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using WebAccounting.Extensions;

namespace WebAccounting.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComputerController : ControllerBase
{
    //по идее тоже должен быть интерфейс
    private readonly ComputerService _service;

    public ComputerController(ComputerService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("GetAll")]
    public ActionResult<IList<ComputerDTO>> GetComputers()
    {
        var computers = _service.GetComputers(10,0);
        if (computers.Count == 0) return NotFound();

        var result = computers.ToDto();
        return Ok(result);
    }

    [HttpGet]
    [Route("GetByID")]
    public ActionResult<ComputerDTO> GetByID(uint id)
    {
        var computer = _service.GetByID(id);
        if (computer == null) return NotFound();

        var result = computer.ToDto();
        return Ok(result);
    }

    [HttpPost]
    [Route("Filters")]
    [AllowAnonymous]
    public ActionResult<IList<ComputerDTO>> Filters([FromBody]ComputerFilterDTO request)
    {
        var computer = request.ToComputer();
        var computers = _service.GetComputers
            (10, 0, computer.Name, computer.Price.ToString(),
            (int)computer.Status, computer.EmployeeID.ToString());
        if(computers.Count == 0) return NotFound();

        var result = computers.ToDto();
        return Ok(result);
    }

    [HttpPost]
    [Route("Create")]
    //по идее для компьютера тоже нужен свой респонс
    public ActionResult<string> Create([FromBody]ComputerCreateDTO request)
    {
        var computer = request.ToComputer();

        if (computer == null) return BadRequest("Неверный статус/данные");

        _service.Create(computer);
        return Ok("Все ок");
    }

    [HttpPut]
    [Route("Change")]
    public ActionResult<string> Update([FromBody]ComputerDTO request)
    {
        var computer = request.ToComputer();
        if (computer == null) return BadRequest("неверные данные");
        _service.Update(computer);
        return Ok("Обновлен успешно");
    }

    [HttpDelete]
    [Route("Delete")]
    public ActionResult Delete(uint id)
    {
        _service.Delete(id);
        return Ok();
    }
}
