﻿using MediatR;
using Application.Core;
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

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result is null) return NotFound();

        if (result.IsSuccess && result.Value is not null) return Ok(result.Value);

        if (result.IsSuccess && result.Value is null) return NotFound();

        return BadRequest(result.Error);
    }
}
