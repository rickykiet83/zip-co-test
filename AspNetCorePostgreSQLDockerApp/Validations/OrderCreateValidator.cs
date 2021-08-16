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
                .MinimumLength(3).WithMessage("Product name minimum length should be 5");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity must be >= 1")
                .GreaterThanOrEqualTo(1).WithMessage("Quantity should be >= 1");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price should be >= 0");
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer Id is required");
        }
    }
}