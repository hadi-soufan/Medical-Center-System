using Application.Staff;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller responsible for managing staff entities.
    /// </summary>
    public class StaffController : BaseApiController
    {
        /// <inheritdoc />
        // GET: /api/staff/get-all-staff
        [HttpGet("get-all-staff")]
        public async Task<IActionResult> GetAllStaff()
        {
            return HandleResult(await Mediator.Send(new StaffList.Query()));
        }

        /// <inheritdoc />
        // GET: /api/staff/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleStaff(Guid id)
        {
            return HandleResult(await Mediator.Send(new StaffDetails.Query { Id = id }));
        }

        /// <inheritdoc />
        // PUT: /api/staff/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(StaffDto staffDto, Guid id)
        {
            return HandleResult(await Mediator.Send(new StaffUpdate.Command { StaffDto = staffDto, Id = id }));  
        }

        /// <inheritdoc />
        // DELETE: /api/staff/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(Guid id)
        {
            return HandleResult(await Mediator.Send(new StaffDelete.Command { Id = id }));
        }
    }
}
