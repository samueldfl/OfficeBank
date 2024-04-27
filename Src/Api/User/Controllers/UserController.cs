using Microsoft.AspNetCore.Mvc;

using Application.User.Handlers.Abst;
using Application.Shared.ResultStates;
using Domain.User.Commands;

namespace Api.User.Controllers;

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
        RootResult result = await _createUserCommandHandler.HandleAsync(
            command,
            cancellationToken
        );

        return result switch
        {
            RootCreatedResult => Created(),
            RootBadRequestResult => BadRequest(result.Body),
            _ => StatusCode(result.Code, result.Body),
        };
    }
}
