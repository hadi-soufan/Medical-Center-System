using MediatR;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Represents a base API controller providing access to the mediator for handling requests.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;

    /// <summary>
    /// Gets the mediator for handling requests.
    /// </summary>
    protected IMediator Mediator => _mediator ??=
        HttpContext.RequestServices.GetService<IMediator>();
}
