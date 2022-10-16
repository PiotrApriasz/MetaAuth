using FluentValidation;
using MetaAuth.Core.Entities.User;

namespace MetaAuth.Client.Validators;

public class MetaAuthUserDataValidator : AbstractValidator<MetaAuthUserData>
{
    public MetaAuthUserDataValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Field Name should not be empty");

        RuleFor(x => x.Surname)
            .NotEmpty()
            .WithMessage("Field Surname should not be empty");

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage("Field Birth Date should not be empty");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Field Email should not be empty")
            .EmailAddress()
            .WithMessage("This field should be filled with email address");

        RuleFor(x => x.Address.Town)
            .NotEmpty()
            .WithMessage("Field Town should not be empty");

        RuleFor(x => x.Address.HomeNumber)
            .NotEmpty()
            .WithMessage("Field Home Number should not be empty");
        
        
        RuleFor(x => x.Address.PostalCode)
            .NotEmpty()
            .WithMessage("Field Postal Code should not be empty");
        
        RuleFor(x => x.Address.Country)
            .NotEmpty()
            .WithMessage("Field Country should not be empty");
        
        RuleFor(x => x.IdCard.IdCardNumber)
            .NotEmpty()
            .WithMessage("Field Id Card Number should not be empty");
        
        RuleFor(x => x.IdCard.PersonalIdNumber)
            .NotEmpty()
            .WithMessage("Field Personal Id Number should not be empty");
    }
    
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<MetaAuthUserData>
            .CreateWithOptions((MetaAuthUserData)model, x => 
                x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}