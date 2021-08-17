using System.Collections.Generic;
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
        }
    }
}