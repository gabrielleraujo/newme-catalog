namespace Newme.Catalog.Application.Validations
{
    public class UploadProductImagesCommandValidation : ProductImagesCommandValidation
    {
        public UploadProductImagesCommandValidation()
        {
            ValidateDescription();
        }
    }
}