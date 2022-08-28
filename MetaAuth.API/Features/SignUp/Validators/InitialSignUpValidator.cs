using FluentValidation;
using MetaAuth.API.Features.SignUp.Requests;

namespace MetaAuth.API.Features.SignUp.Validators;

public class InitialSignUpValidator : AbstractValidator<InitialSignUpRequest>
{
    public InitialSignUpValidator()
    {
        RuleFor(x => x.AppName)
            .Matches(@"[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)")
            .WithMessage("Given web app name is incorrect")
            .NotEmpty()
            .WithMessage("Web app name must be provided")
            .NotNull()
            .WithMessage("Web app name must be provided");
        
        RuleFor(x => x.UserIdentificator)
            .NotEmpty()
            .WithMessage("User identificator must be provided")
            .NotNull()
            .WithMessage("User identificator must be provided");
        
        RuleFor(x => x.RequiredUserData)
            .NotEmpty()
            .WithMessage("List of required user data must be provided")
            .NotNull()
            .WithMessage("List of required user data must be provided");
            
    }
}