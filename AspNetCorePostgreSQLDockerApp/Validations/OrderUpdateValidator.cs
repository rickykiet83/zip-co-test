using AspNetCorePostgreSQLDockerApp.Dtos;
using FluentValidation;

namespace AspNetCorePostgreSQLDockerApp.Validations
{
    public class OrderUpdateValidator : AbstractValidator<OrderForUpdateDto>
    {
        public OrderUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Id invalid.");
            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("Customer Id is invalid.");
            RuleFor(x => x.Product)
                .NotEmpty().WithMessage("Product is required")
                .MaximumLength(150).WithMessage("Product name maximum length should be 150.")
                .MinimumLength(3).WithMessage("Product name minimum length should be 5.");
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity should be >= 0.");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price should be >= 0.");
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Order status is not supported.");
        }
    }
}