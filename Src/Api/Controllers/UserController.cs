using Microsoft.AspNetCore.Mvc;

using Application.Shared.Identities;
using Application.User.Auth.Handlers.Abstractions;
using Domain.User.Commands;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public sealed class UserController : ControllerBase
{
    private readonly ICreateUserCommandHandler _createUserCommandHandler;

    public UserController(ICreateUserCommandHandler createUserCommandHandler)
    {
        _createUserCommandHandler = createUserCommandHandler;
    }

    [HttpPost("/register")]
    public async Task<IActionResult> Register(
        CreateUserCommand command,
        CancellationToken cancellationToken
    )
    {
        Result result = await _createUserCommandHandler.HandleAsync(
            command,
            cancellationToken
        );

        return result.Code switch
        {
            RootStatusCode.CREATED => Created(),
            _ => StatusCode(500, result.Body),
        };
    }
}
