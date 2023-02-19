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
    private static int _pageNumber;
    public ComputerController(ComputerService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("GetAll")]
    public ActionResult<IList<ComputerDTO>> GetComputers()
    {
        var computers = _service.GetComputers(10,0);
        if (computers.Count == 0) return NotFound(new List<ComputerDTO>());

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

        if (computer == null) return BadRequest("Неверные данные");

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
    public ActionResult<string> Delete(uint id)
    {
        var comp = _service.GetByID(id);
        if (comp == null) return NotFound($"Компьютер с id:{id} не найден");
        _service.Delete(id);
        return Ok("Компьютер удален");
    }

    [HttpGet]
    [Route("Next")]
    public ActionResult<IList<ComputerDTO>> GetNext()
    {
        IList<Computer> computers;
        var count = _service.Count();
        if (_pageNumber * 10 > count - 10)
        {
            computers = _service.GetComputers(10, _pageNumber * 10);
        }
        else
        {
            computers = _service.GetComputers(10, ++_pageNumber * 10);
        }
        var result = computers.ToDto();
        return Ok(result);
    }

    [HttpGet]
    [Route("Previous")]
    public ActionResult<IList<ComputerDTO>> GetPrevious()
    {
        IList<Computer> computers;
        if (_pageNumber == 0)
        {
            computers = _service.GetComputers(10, 0);
        }
        else
        {
            computers = _service.GetComputers(10, --_pageNumber * 10);
        }
        var result = computers.ToDto();
        return Ok(result);
    }    
}
