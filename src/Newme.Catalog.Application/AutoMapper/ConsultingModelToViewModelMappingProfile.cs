using AutoMapper;
using Newme.Catalog.Application.ViewModels;
using Newme.Catalog.Application.Consulting.ConsultingModels;

namespace Newme.Catalog.Application.AutoMapper
{
    public class ConsultingModelToViewModelMappingProfile : Profile
    {
        public ConsultingModelToViewModelMappingProfile()
        {
            CreateMap<ProductConsultingModel, ReadProductViewModel>();
            CreateMap<PromotionConsultingModel, ReadPromotionViewModel>();
            CreateMap<GenderConsultingModel, ReadGenderViewModel>();
            CreateMap<CategoryConsultingModel, ReadCategoryViewModel>();
            CreateMap<ColorConsultingModel, ReadColorViewModel>();
            CreateMap<SizeConsultingModel, ReadSizeViewModel>();
        }
    }
}