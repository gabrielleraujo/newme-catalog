using System.Linq.Expressions;
using Newme.Catalog.Application.Consulting.ConsultingModels;

namespace Newme.Catalog.Application.Consulting.Repositories
{
    public interface IBaseQueryRepository<T> where T : ConsultingModel
    {   
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<bool> ExistsByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllByAsync<TValueMatch>(
            TValueMatch value, Expression<Func<T, TValueMatch>> expression);
        Task<IList<T>> GetByFilterAsync(Expression<Func<T, bool>> expression);
        Task<bool> ExistsByAsync <TValueMatch>(
            TValueMatch value, Expression<Func<T, TValueMatch>> expression);
        Task AddAsync(T value);
        Task UpdateAsync<TValueUpdate>(
            Guid id, TValueUpdate newValue, Expression<Func<T, TValueUpdate>> expression);

        Task DeleteAsync(Guid id);
        Task ActivateAsync(Guid id);
        Task DeactivateAsync(Guid id);
    }
}
