using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.Login;
using Project.Services.Contracts;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        // Constructor dependency injection
        public LoginController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // POST api/login/create (Login oluştur)
        [HttpPost("create")]
        public async Task<IActionResult> CreateLogin([FromBody] LoginDtoInsertion loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Invalid login data.");
            }

            // ServiceManager üzerinden LoginService'e erişiyoruz
            var createdLogin = await _serviceManager.LoginService.CreateLogin(loginDto);

            return CreatedAtAction(nameof(GetLoginById), new { id = createdLogin.LoginId }, createdLogin);
        }

        // GET api/login/{id} (Login bilgilerini al)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoginById(int id)
        {
            var login = await _serviceManager.LoginService.GetLoginById(id, trackChanges: false);
            if (login == null)
            {
                return NotFound($"Login with id {id} not found.");
            }
            return Ok(login);
        }

        // PUT api/login/update/{id} (Login güncelle)
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateLogin(int id, [FromBody] LoginDtoUpdate loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Invalid login data.");
            }

            await _serviceManager.LoginService.UpdateLogin(id,loginDto,false);

            return NoContent(); // NoContent() başarılı güncelleme sonrası döndürülür
        }

        // POST api/login/authenticate (Kullanıcı giriş yapacak)
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Invalid login credentials.");
            }

            var loginResponse = await _serviceManager.LoginService.AuthenticateUser(loginDto);

            if (loginResponse == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(loginResponse); // Token ve role ile birlikte başarılı giriş
        }
    }
}
