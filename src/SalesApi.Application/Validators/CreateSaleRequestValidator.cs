using FluentValidation;
using SalesApi.Application.Requests;

namespace SalesApi.Application.Validators;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("Sale number is required.");
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer ID is required.");
        RuleFor(x => x.BranchId).NotEmpty().WithMessage("Branch ID is required.");
        RuleFor(x => x.Items).NotEmpty().WithMessage("At least one sale item is required.");
        RuleForEach(x => x.Items).SetValidator(new CreateSaleItemRequestValidator());
    }

    public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
    {
        public CreateSaleItemRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
            RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Unit price must be greater than 0.");
        }
    }
}
