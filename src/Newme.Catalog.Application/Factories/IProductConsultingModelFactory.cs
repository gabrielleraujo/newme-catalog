using Newme.Catalog.Domain.Entities;
using Newme.Catalog.Application.Consulting.ConsultingModels;

namespace Newme.Catalog.Application.Factories
{
    public interface IProductConsultingModelFactory 
    {
        Task<ProductConsultingModel> Build(Product product);
    }
}