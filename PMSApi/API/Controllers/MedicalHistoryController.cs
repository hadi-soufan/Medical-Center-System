using Application.MedicalHistoreis;
using Application.MedicalHistories;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing medical history.
    /// </summary>
    public class MedicalHistoryController : BaseApiController
    {
        /// <inheritdoc />
        // GET: /api/medicalhistory
        [HttpGet]
        public async Task<ActionResult<List<MedicalHistory>>> GetMedicalHistories()
        {
            return await Mediator.Send(new MedicalHistoryList.Query());
        }

        /// <inheritdoc />
        // GET: /api/medicalhistory
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalHistory>> GetAppoitment(Guid id)
        {
            return await Mediator.Send(new MedicalHistoryDetails.Query { Id = id });
        }

        /// <inheritdoc />
        // POST: /api/medicalhistory
        [HttpPost]
        public async Task<IActionResult> CreateMedicalHistory(MedicalHistory medicalHistory)
        {
            return Ok(await Mediator.Send(new MedicalHistoryCreate.Command { MedicalHistory = medicalHistory }));
        }

        /// <inheritdoc />
        // PUT: /api/medicalhistory
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalHistory(Guid id, MedicalHistory medicalHistory)
        {
            medicalHistory.MedicalHistoryId = id;

            return Ok(await Mediator.Send(new MedicalHistoryUpdate.Command { MedicalHistory = medicalHistory }));
        }

        /// <inheritdoc />
        // DELETE: /api/medicalhistory
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalHistory(Guid id)
        {
            return Ok(await Mediator.Send(new MedicalHistoryDelete.Command { Id = id }));
        }
    }
}
