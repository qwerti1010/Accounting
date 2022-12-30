using DBLibrary;
using DBLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace WebAccounting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly ComputerService _service;

        public ComputerController()
        {
            var context = new DbConnect();
            _service = new ComputerService(context);
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<IList<Computer>> GetComputers()
        {
            var result = _service.GetComputers(10,0);
            if (result.Count == 0) return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetByID")]
        public ActionResult<Computer> GetByID(uint id)
        {
            var result = _service.GetByID(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("Filters")]
        public ActionResult<IList<Computer>> Filters(string? nameFilter,
            string? priceFilter, int statusFilter, string? emplID)
        {
            var result = _service.GetComputers(10, 0, nameFilter, priceFilter, statusFilter, emplID);
            if(result.Count == 0) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        //по идее для компьютера тоже нужен свой респонс
        public ActionResult Create(Computer computer)
        {
            _service.Create(computer);
            return Ok();
        }

        [HttpPut]
        [Route("Change")]
        public ActionResult Update(Computer computer)
        {
            _service.Update(computer);
            return Ok(computer);
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(uint id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}
