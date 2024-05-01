using Application.Shared.ResultStates;
using Application.User.Handlers.Abst;
using Domain.User.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Api.User.Controllers;

[Route("Api/[controller]")]
[ApiController]
public sealed class UserController(ICreateUserCommandHandler createUserCommandHandler)
    : ControllerBase
{
    private readonly ICreateUserCommandHandler _createUserCommandHandler =
        createUserCommandHandler;

    [HttpPost("register")]
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
