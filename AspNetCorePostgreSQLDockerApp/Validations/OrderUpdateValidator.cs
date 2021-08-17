using AspNetCorePostgreSQLDockerApp.Dtos;
using FluentValidation;

namespace AspNetCorePostgreSQLDockerApp.Validations
{
    public class OrderUpdateValidator : AbstractValidator<OrderForUpdateDto>
    {
        public OrderUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .GreaterThanOrEqualTo(0).WithMessage("Id invalid");
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer Id is required")
                .GreaterThan(0).WithMessage("Customer Id is invalid");
        }
    }
}