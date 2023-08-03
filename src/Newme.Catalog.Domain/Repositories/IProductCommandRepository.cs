using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Domain.Repositories;
public interface IProductCommandRepository
{
    Task RegisterProductAsync(Product product);
    void Update(Product product);
    void Delete(ProductId product);

    Task RegisterCategoryAsync(Category category);
    void UpdateCategory(Category category);

    Task RegisterColorAsync(Color color);
    void UpdateColor(Color color);

    Task RegisterSizeAsync(Size size);
    void UpdateSize(Size size);

    Task<bool> Commit();
    void Dispose();

    #region consulting
    Task<T> FindByAsync<T>(Func<T, bool> predicate) where T : class;
    Task<List<T>> GetByAsync<T>(Func<T, bool> predicate) where T : class;
    Task<List<T>> GetAllAsync<T>() where T : class;
    #endregion
}