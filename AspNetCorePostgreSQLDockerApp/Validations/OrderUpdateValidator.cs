using FluentValidation;

namespace AspNetCorePostgreSQLDockerApp.Validations
{
    public class OrderUpdateValidator : OrderCreateValidator
    {
        public OrderUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .GreaterThan(0).WithMessage("Id invalid")
                ;
        }
    }
}