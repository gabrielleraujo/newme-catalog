using Newme.Catalog.Application.Consulting.ConsultingModels;

namespace Newme.Catalog.Application.Consulting.Repositories;
public interface IProductQueryRepository : IBaseQueryRepository<ProductConsultingModel>
{
    Task AddImagesAsync(Guid id, IList<string> images);
    Task<ProductConsultingModel> GetByNameAsync(string name);
    Task<IEnumerable<ProductConsultingModel>> SearchByNameAsync(string name);
    Task UpdateManyAsync(Guid id, ProductConsultingModel newModel);
}