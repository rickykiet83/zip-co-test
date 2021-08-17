using AspNetCorePostgreSQLDockerApp.Constants;
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
            RuleFor(x => x.GetTotalOrderInProgress())
                .LessThanOrEqualTo(SystemConstants.TotalInProgressAllow)
                .WithMessage("Maximum of 4 InProgress orders for each customer allowed to be stored");
        }
    }
}