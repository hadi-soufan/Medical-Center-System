using MediatR;
using Application.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Represents a base API controller providing access to the mediator for handling requests.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;

    /// <summary>
    /// Gets the mediator for handling requests.
    /// </summary>
    protected IMediator Mediator => _mediator ??=
        HttpContext.RequestServices.GetService<IMediator>();

    /// <summary>
    /// Handles the result of an operation.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="result">The result to handle.</param>
    /// <returns>An appropriate HTTP response based on the result.</returns>
    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result is null) return NotFound();

        if (result.IsSuccess && result.Value is not null) return Ok(result.Value);

        if (result.IsSuccess && result.Value is null) return NotFound();

        return BadRequest(result.Error);
    }
}
