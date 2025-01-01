using FitnessApp.Interface;
using FitnessApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramRepository _repo;

        public ProgramController(IProgramRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{programCode}")]
        public ActionResult<FitnessProgram> Get(string programCode)
        {
            try
            {
                return Ok(_repo.GetProgram(programCode));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<FitnessProgram> Post([FromBody] FitnessProgram program)
        {
            try
            {
                _repo.AddProgram(program);
                return CreatedAtAction(nameof(Get),
                    new { programCode = program.ProgramCode },
                    program);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{programCode}")]
        public IActionResult Put(string programCode, [FromBody] FitnessProgram program)
        {
            if (program == null)
            {
                return BadRequest();
            }

            //DOE DIT WEG INDIEN JE PROGRAMCODE OOK IN DE JSON MOET SCHRIJVEN
            program.ProgramCode = programCode;

            if (!_repo.ExistsProgram(programCode))
            {
                _repo.AddProgram(program);
                return CreatedAtAction(nameof(Get), new { programCode = program.ProgramCode }, program);
            }
            _repo.UpdateProgram(program);
            return NoContent();
        }
    }
}
