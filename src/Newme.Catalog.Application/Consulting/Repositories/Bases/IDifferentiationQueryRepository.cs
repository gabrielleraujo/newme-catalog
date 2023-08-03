using Newme.Catalog.Application.Consulting.ConsultingModels;

namespace Newme.Catalog.Application.Consulting.Repositories
{
    public interface IDifferentiatialQueryRepository<T>
        where T : DifferentialConsultingModel
    {
        Task<T> GetByNameAsync(string name);
        Task<bool> ExistsAsync(string value);
        Task<IEnumerable<T>> SearchByNameAsync(string name);
        Task UpdateManyAsync(Guid id, T newModel);
    }
}