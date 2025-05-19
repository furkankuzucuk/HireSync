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

            await _serviceManager.LoginService.UpdateLogin(id,loginDto,true);

            return NoContent(); // NoContent() başarılı güncelleme sonrası döndürülür
        }

        // POST api/login/authenticate (Kullanıcı giriş yapacak)
        [HttpPost("authenticate")]
public async Task<IActionResult> AuthenticateUser([FromBody] LoginAuthenticationDto loginDto)
{
    if (loginDto == null)
        return BadRequest("Invalid login credentials.");

    var loginResponse = await _serviceManager.LoginService.AuthenticateUser(loginDto);

    if (loginResponse == null)
        return Unauthorized("Invalid username or password.");

    return Ok(new
    {
        token = loginResponse.Token,
        role = loginResponse.Role,
        userId = loginResponse.UserId // ✅ ekleniyor
    });
}


        [HttpPost("forgot-password")] //emaili giren kullanıcı bu isteği göndermeli
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (forgotPasswordDto == null || string.IsNullOrEmpty(forgotPasswordDto.Email))
            {
                return BadRequest("Email is required.");
            }

            var result = await _serviceManager.LoginService.GeneratePasswordResetToken(forgotPasswordDto.Email);
            
            if (!result)
            {
                // Güvenlik nedeniyle her zaman başarılı gibi gösteriyoruz
                return Ok("If your email is registered, you will receive a password reset link.");
            }

            return Ok("If your email is registered, you will receive a password reset link.");
        }

    // POST api/login/reset-password
       [HttpPost("reset-password")] //şifre sıfırlama sayfasında kullanıcı bu isteği atar
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto == null)
            {
                return BadRequest("Invalid reset data.");
            }

            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            var result = await _serviceManager.LoginService.ResetPassword(resetPasswordDto.Token, resetPasswordDto.NewPassword);
            
            if (!result)
            {
                return BadRequest("Invalid or expired token.");
            }

            return Ok("Password has been reset successfully.");
        }
    }
}
