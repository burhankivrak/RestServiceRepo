using FitnessApp.Interface;
using FitnessApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private IProgramRepository repo;

        public ProgramController(IProgramRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("{programCode}")]
        public ActionResult<FitnessProgram> Get(string programCode)
        {
            try
            {
                return Ok(repo.GetProgram(programCode));
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
                repo.AddProgram(program);
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

            if (!repo.ExistsProgram(programCode))
            {
                repo.AddProgram(program);
                return CreatedAtAction(nameof(Get), new { programCode = program.ProgramCode }, program);
            }
            repo.UpdateProgram(program);
            return NoContent();
        }
    }
}
