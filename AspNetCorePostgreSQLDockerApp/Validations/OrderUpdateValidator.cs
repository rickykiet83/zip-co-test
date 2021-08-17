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
                .GreaterThanOrEqualTo(0).WithMessage("Id invalid")
                ;
        }
    }
}