using Application.Patients;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller responsible for managing patient entities.
    /// </summary>
    public class PatientController : BaseApiController
    {
        /// <inheritdoc />
        // GET: /api/patients/all-patients
        [HttpGet("all-patients")]
        public async Task<IActionResult> GetPatients()
        {
            return HandleResult(await Mediator.Send(new PatientList.Query()));
        }

        /// <inheritdoc />
        // GET: /api/patients/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(Guid id)
        {
            return HandleResult(await Mediator.Send(new PatientDetails.Query { Id = id }));
        }

        /// <inheritdoc />
        // DELETE: /api/patient/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            return HandleResult(await Mediator.Send(new PatientDelete.Command { Id = id }));
        }

    }
}
