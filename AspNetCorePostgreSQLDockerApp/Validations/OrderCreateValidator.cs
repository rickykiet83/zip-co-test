using AspNetCorePostgreSQLDockerApp.Dtos;
using FluentValidation;

namespace AspNetCorePostgreSQLDockerApp.Validations
{
    public class OrderCreateValidator : AbstractValidator<OrderDto>
    {
        public OrderCreateValidator()
        {
            RuleFor(x => x.Product)
                .NotEmpty().WithMessage("Product is required")
                .MaximumLength(150).WithMessage("Product name maximum length should be 150")
                .MinimumLength(5).WithMessage("Product name minimum length should be 5");
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .GreaterThanOrEqualTo(1).WithMessage("Quantity should >= 1");
            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer Id is required");
        }
    }
}