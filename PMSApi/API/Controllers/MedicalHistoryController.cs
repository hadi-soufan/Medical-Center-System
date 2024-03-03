using Application.MedicalHistoreis;
using Application.MedicalHistories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing medical history.
    /// </summary>
    public class MedicalHistoryController : BaseApiController
    {
        /// <inheritdoc />
        // GET: /api/medicalhistory/all-medical-histories
        [HttpGet("all-medical-histories")]
        public async Task<ActionResult<List<MedicalHistory>>> GetMedicalHistories()
        {
            return HandleResult(await Mediator.Send(new MedicalHistoryList.Query()));
        }

        /// <inheritdoc />
        // GET: /api/medicalhistory/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalHistory>> GetAppoitment(Guid id)
        {
            return HandleResult(await Mediator.Send(new MedicalHistoryDetails.Query { Id = id }));
        }

        /// <inheritdoc />
        // POST: /api/medicalhistory/create-new-medical-history
        [HttpPost("create-new-medical-history")]
        public async Task<IActionResult> CreateMedicalHistory(MedicalHistory medicalHistory)
        {
            return HandleResult(await Mediator.Send(new MedicalHistoryCreate.Command { MedicalHistory = medicalHistory }));
        }

        /// <inheritdoc />
        // PUT: /api/medicalhistory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalHistory(Guid id, MedicalHistory medicalHistory)
        {
            medicalHistory.MedicalHistoryId = id;

            return HandleResult(await Mediator.Send(new MedicalHistoryUpdate.Command { MedicalHistory = medicalHistory }));
        }

        /// <inheritdoc />
        // DELETE: /api/medicalhistory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalHistory(Guid id)
        {
            return HandleResult(await Mediator.Send(new MedicalHistoryDelete.Command { Id = id }));
        }
    }
}
