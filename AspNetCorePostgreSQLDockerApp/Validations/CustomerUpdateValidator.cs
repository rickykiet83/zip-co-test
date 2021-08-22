using AspNetCorePostgreSQLDockerApp.Dtos;
using FluentValidation;

namespace AspNetCorePostgreSQLDockerApp.Validations
{
    public class CustomerUpdateValidator : AbstractValidator<CustomerUpdateDto>
    {
        public CustomerUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email invalid")
                .NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required");
            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required");
        }
    }
}