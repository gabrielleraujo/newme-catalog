using Newme.Catalog.Application.Commands;

namespace Newme.Catalog.Application.Validations
{
    public class RegisterNewProductCommandValidation : ProductCommandValidation<RegisterNewProductCommand>
    {
        public RegisterNewProductCommandValidation()
        {
            ValidateName();
            ValidateDescription();
            ValidateCurrency();
            ValidateAmount();
            ValidateStock();
        }
    }
}