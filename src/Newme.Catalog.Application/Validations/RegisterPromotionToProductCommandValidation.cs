using Newme.Catalog.Application.Commands;

namespace Newme.Catalog.Application.Validations
{
    public class RegisterPromotionToProductCommandValidation : PromotionToProductCommandValidation<RegisterPromotionToProductCommand>
    {
        public RegisterPromotionToProductCommandValidation()
        {
            ValidateName();
            ValidateDescription();
            ValidatePercentage();
            ValidateCurrency();
            ValidateAmount();
            Validatestart();
        }
    }
}