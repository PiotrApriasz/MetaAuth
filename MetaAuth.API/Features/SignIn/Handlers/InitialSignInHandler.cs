using MediatR;
using MetaAuth.API.Features.SignIn.Requests;
using MetaAuth.API.Features.SignIn.Services;

namespace MetaAuth.API.Features.SignIn.Handlers;

public class InitialSignInHandler : IRequestHandler<InitialSignInRequest, IResult>
{
    private readonly ISignInService _signInService;

    public InitialSignInHandler(ISignInService signInService)
    {
        _signInService = signInService;
    }

    public Task<IResult> Handle(InitialSignInRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}