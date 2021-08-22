using AspNetCorePostgreSQLDockerApp.Dtos;
using FluentValidation;

namespace AspNetCorePostgreSQLDockerApp.Validations
{
    public class CustomerCreateValidator : AbstractValidator<CustomerCreateDto>
    {
        public CustomerCreateValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required");
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email invalid")
                .NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required");
        }
    }
}