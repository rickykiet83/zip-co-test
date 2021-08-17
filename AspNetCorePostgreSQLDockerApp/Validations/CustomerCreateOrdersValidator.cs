using AspNetCorePostgreSQLDockerApp.Dtos;
using FluentValidation;

namespace AspNetCorePostgreSQLDockerApp.Validations
{
    public class CustomerCreateOrdersValidator : AbstractValidator<CustomerCreateOrdersDto>
    {
        public CustomerCreateOrdersValidator()
        {
            RuleForEach(x => x.OrderDtos).SetValidator(new OrderCreateValidator());
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer Id is required");
            RuleFor(x => x.OrderDtos)
                .NotEmpty().WithMessage("Orders is required");
        }
    }
}