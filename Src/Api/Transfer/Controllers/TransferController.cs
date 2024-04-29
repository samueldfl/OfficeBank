using Application.Shared.ResultStates;
using Application.Transaction.Commands;
using Application.Transaction.Handlers;
using Application.Transaction.Handlers.Abst;
using Application.Transfer.Handlers.Abst;
using Domain.Transfer.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Transfer.Controllers;

[Route("[controller]")]
[ApiController]
public sealed class TransferController : ControllerBase
{
    private readonly ICreateTransferCommandHandler _createTransferCommandHandler;

    private readonly IDepositCommandHandler _depositCommandHandler;

    public TransferController(
        ICreateTransferCommandHandler createTransferCommandHandler,
        IDepositCommandHandler depositCommandHandler
    )
    {
        _createTransferCommandHandler = createTransferCommandHandler;
        _depositCommandHandler = depositCommandHandler;
    }

    [EnableRateLimiting("fixed")]
    [HttpPost("create-transfer")]
    public async Task<IActionResult> Create(
        CreateTransferCommand command,
        CancellationToken cancellationToken
    )
    {
        RootResult result = await _createTransferCommandHandler.HandleAsync(
            command,
            cancellationToken
        );

        return result switch
        {
            RootCreatedResult => Created(),
            RootBadRequestResult => BadRequest(),
            _ => StatusCode(result.Code, result.Body)
        };
    }

    [HttpPost("")]
    public async Task<IActionResult> Deposit(
        DepositCommand command,
        CancellationToken cancellationToken
    )
    {
        RootResult result = await _depositCommandHandler.HandleAsync(
            command,
            cancellationToken
        );

        return result switch
        {
            RootCreatedResult => Created(),
            RootBadRequestResult => BadRequest(),
            _ => StatusCode(result.Code, result.Body)
        };
    }
}
