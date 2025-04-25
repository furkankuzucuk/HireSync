using Microsoft.AspNetCore.Mvc;
using Project.Entities.DataTransferObjects.Candidate;
using Project.Services.Contracts;

namespace Project.Presentation.Controller
{
    [ApiController]
    [Route("api/candidates")]
    public class CandidateController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CandidateController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // POST api/candidates/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateCandidate([FromBody] CandidateDtoInsertion candidateDto)
        {
            if (candidateDto == null)
            {
                return BadRequest("Invalid candidate data.");
            }

            var createdCandidate = await _serviceManager.CandidateService.CreateCandidate(candidateDto);

            return CreatedAtAction(nameof(GetCandidateById), new { id = createdCandidate.CandidateId }, createdCandidate);
        }

        // GET api/candidates/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidateById(int id, [FromQuery] bool trackChanges = false)
        {
            var candidate = await _serviceManager.CandidateService.GetCandidateById(id, trackChanges);
            if (candidate == null)
            {
                return NotFound($"Candidate with id {id} not found.");
            }

            return Ok(candidate);
        }

        // PUT api/candidates/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCandidate(int id, [FromBody] CandidateDtoUpdate candidateDto)
        {
            if (candidateDto == null)
            {
                return BadRequest("Invalid candidate data.");
            }

            await _serviceManager.CandidateService.UpdateCandidate(id, candidateDto);
            return NoContent();
        }

        // POST api/candidates/authenticate
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateCandidate([FromBody] CandidateAuthenticationDto candidateAuthDto)
        {
            if (candidateAuthDto == null)
            {
                return BadRequest("Invalid credentials.");
            }

            var candidate = await _serviceManager.CandidateService.AuthenticateCandidate(candidateAuthDto);

            if (candidate == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(candidate); // Dönen değer burada başarılı giriş sonrası token olabilir veya basit olarak aday bilgileri.
        }
    }
}
