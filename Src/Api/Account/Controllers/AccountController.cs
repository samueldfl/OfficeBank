using Application.Account.Handlers.Abst;
using Application.Shared.ResultStates;
using Domain.Account.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Api.Account.Controllers;

[Route("Api/[controller]")]
[ApiController]
public sealed class AccountController(
    IRegisterAccountCommandHandler createAccountCommandHandler
) : ControllerBase
{
    private readonly IRegisterAccountCommandHandler _createAccountCommandHandler =
        createAccountCommandHandler;

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        RegisterAccountCommand command,
        CancellationToken cancellationToken
    )
    {
        RootResult result = await _createAccountCommandHandler.HandleAsync(
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
