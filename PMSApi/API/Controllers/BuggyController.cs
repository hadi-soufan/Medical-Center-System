using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for handling buggy scenarios.
    /// </summary>
    public class BuggyController : BaseApiController
    {
        /// <summary>
        /// Returns a 404 Not Found response.
        /// </summary>
        /// <returns>404 Not Found response.</returns>
        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            return NotFound();
        }

        /// <summary>
        /// Returns a 400 Bad Request response with a specified error message.
        /// </summary>
        /// <returns>400 Bad Request response with an error message.</returns>
        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest("This is a bad request");
        }

        /// <summary>
        /// Throws an exception to simulate a server error.
        /// </summary>
        /// <returns>Server error response.</returns>
        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            throw new Exception("This is a server error");
        }

        /// <summary>
        /// Returns a 401 Unauthorized response.
        /// </summary>
        /// <returns>401 Unauthorized response.</returns>
        [HttpGet("unauthorised")]
        public ActionResult GetUnauthorised()
        {
            return Unauthorized();
        }
    }
}