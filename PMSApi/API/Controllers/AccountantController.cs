using Application.Accountants;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for managing accountants.
    /// </summary>
    public class AccountantController : BaseApiController
    {
        /// <inheritdoc />
        // GET: /api/accountants/get-all-accountants
        [HttpGet("get-all-accountants")]
        public async Task<IActionResult> AllAccountants()
        {
            return HandleResult(await Mediator.Send(new AccoutantList.Query()));
        }

        /// <inheritdoc />
        // GET: /api/accountants/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAccountant(Guid id)
        {
            return HandleResult(await Mediator.Send(new AccoutantDetails.Query { Id = id }));
        }

        /// <inheritdoc />
        // GET: /api/accountants/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccountant(AccoutantDto accoutantDto, Guid id)
        {
            return HandleResult(await Mediator.Send(new AccoutantUpdate.Command { AccoutantDto = accoutantDto, Id = id }));
        }

        /// <inheritdoc />
        // GET: /api/accountants/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountant(Guid id)
        {
            return HandleResult(await Mediator.Send(new AccoutantDelete.Command { Id = id }));
        }
    }
}
