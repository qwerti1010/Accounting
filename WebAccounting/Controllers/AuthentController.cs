using DBLibrary;
using DBLibrary.Entities;
using DBLibrary.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Responses;
using Services.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAccounting.Extensions;

namespace WebAccounting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentController : ControllerBase
    {
        private readonly EmployeeService _service;
        private readonly DBService _dbservice;
        private readonly IConfiguration _configuration;

        public AuthentController(EmployeeService service, DBService db, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
            _dbservice = db;
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult<EmployeeDTO> Login([FromBody]EmpLoginDTO request)
        {
            var response = _service.Login(request.Login!, request.Password!);
            if (!response.IsSuccess) return Unauthorized(response.Message);
            var employee = response.Employee!;
            var result = employee.ToDto();
            //var tokenString = MakeToken(employee);
            return result;            
        }

        [Route("Registration")]
        [HttpPost]
        public ActionResult<string> Registration([FromBody] EmpRegDTO request)
        {
            var result = _service.Registration(new Employee
            {
                Name = request.Name,
                Login = request.Login,
                Phone = request.Phone,
                Position = PositionEnum.User,
                Password = request.Password
            });
            if(!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result.Message);            
        }

        [Route("CreateDb")]
        [HttpGet]
        public ActionResult<string> CreateDb()
        {
            var result = _dbservice.Create();
            return Ok(result);
        }
        

        [HttpGet]
        [Route("Print")]
        public ActionResult Print()
        {
            

            return Ok();
        }

        private string MakeToken(Employee employee)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Name!),
                new Claim(ClaimTypes.MobilePhone, employee.Phone!),
                new Claim(ClaimTypes.Role, employee.Position.ToString())
            };
            var token = new JwtSecurityToken
            (
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
