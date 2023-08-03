namespace Newme.Catalog.Application.InputModels
{
    public class RegisterNewCategoryInputModel : RegisterNewProductDifferentialInputModel
    {
        public RegisterNewCategoryInputModel(string name) : base(name)
        {
        }
    }
}