using AutoMapper;
using Newme.Catalog.Application.Commands;
using Newme.Catalog.Application.InputModels;

namespace Newme.Catalog.Application.AutoMapper
{
    public class InputModelToCommandMappingProfile : Profile
    {
        public InputModelToCommandMappingProfile()
        {
            CreateMap<RegisterNewProductInputModel, RegisterNewProductCommand>()
                .ConstructUsing(c => new RegisterNewProductCommand(
                    c.Name, c.Currency, c.Amount, c.Description,
                    c.Gender, c.Category, c.Color, c.Size,
                    c.Stock));

            CreateMap<RegisterPromotionToProductInputModel, RegisterPromotionToProductCommand>()
                .ConstructUsing(c => new RegisterPromotionToProductCommand(
                    c.ProductId,
                    c.Name,
                    c.Start,
                    c.End,
                    c.Currency,
                    c.Amount,
                    c.Percentage,
                    c.Description));

            CreateMap<RegisterNewCategoryInputModel, RegisterNewCategoryCommand>()
                .ConstructUsing(c => new RegisterNewCategoryCommand(
                    c.Name));

            CreateMap<RegisterNewColorInputModel, RegisterNewColorCommand>()
                .ConstructUsing(c => new RegisterNewColorCommand(
                    c.Name));

            CreateMap<RegisterNewSizeInputModel, RegisterNewSizeCommand>()
                .ConstructUsing(c => new RegisterNewSizeCommand(
                    c.Name));

            CreateMap<FixProductNameAndDescriptionInputModel, FixProductNameAndDescriptionCommand>()
                .ConstructUsing(c => new FixProductNameAndDescriptionCommand(
                    c.Id,
                    c.Name, 
                    c.Description));

            CreateMap<ChangeProductPriceInputModel, ChangeProductPriceCommand>()
                .ConstructUsing(c => new ChangeProductPriceCommand(
                    c.Id,
                    c.Currency,
                    c.Amount));

            CreateMap<RegisterNewProductDifferantialByCodeInputModel, RegisterNewProductByCodeCommand>()
                .ConstructUsing(c => new RegisterNewProductByCodeCommand(
                    c.Sku,
                    c.ColorId,
                    c.SizeId,
                    c.Stock));

            CreateMap<UploadProductImagesInputModel, UploadProductImagesCommand>()
                .ConstructUsing(c => new UploadProductImagesCommand(
                    c.ProductId,
                    c.Images));
        }
    }
}