using Application.Nurses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing nurses.
    /// </summary>
    public class NurseController : BaseApiController
    {
        /// <inheritdoc />
        // GET: /api/nurse/get-all-nurses
        [HttpGet("get-all-nurses")]
        public async Task<IActionResult> GetAllNurses()
        {
            return HandleResult(await Mediator.Send(new NurseList.Query()));
        }

        /// <inheritdoc />
        // GET: /api/nurse/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleNurse(Guid id)
        {
            return HandleResult(await Mediator.Send(new NurseDetails.Query{ Id = id}));
        }

        /// <inheritdoc />
        // GET: /api/nurse/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNure(NurseDto nurseDto, Guid id)
        {
            return HandleResult(await Mediator.Send(new NurseUpdate.Command{ NurseDto = nurseDto, Id = id}));
        }

        /// <inheritdoc />
        // GET: /api/nurse/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNurse(Guid id)
        {
            return HandleResult(await Mediator.Send(new NurseDelete.Command{ Id = id}));
        }
    }
}
