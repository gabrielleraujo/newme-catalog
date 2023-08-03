using Newme.Catalog.Application.Commands;

namespace Newme.Catalog.Application.Validations
{
    public class RegisterNewProductDifferentialCommandValidation : ProductDifferentialCommandValidation<DifferentialCommand>
    {
        public RegisterNewProductDifferentialCommandValidation()
        {
            ValidateName();
        }
    }
}