using Application.Doctors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller responsible for managing doctor entities.
    /// </summary>
    public class DoctorController : BaseApiController
    {
        /// <inheritdoc />
        // GET: /api/doctor/all-doctors
        [HttpGet("all-doctors")]
        public async Task<IActionResult> GetDoctors()
        {
            return HandleResult(await Mediator.Send(new DoctorList.Query()));
        }


        /// <inheritdoc />
        // GET: /api/doctor/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctor(Guid id)
        {
            return HandleResult(await Mediator.Send(new DoctorDetails.Query { Id = id }));
        }

        /// <inheritdoc />
        // PUT: /api/doctor/
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> UpdateDoctor(Guid id, DoctorDto doctorDto)
        {
            return HandleResult(await Mediator.Send(new DoctorUpdate.Command { DoctorId = id, DoctorDto = doctorDto })); 
            
        }

        /// <inheritdoc />
        // DELETE: /api/doctor/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocotr(Guid id)
        {
            return HandleResult(await Mediator.Send(new DoctorDelete.Command { DoctorId = id }));
        }
    }
}
