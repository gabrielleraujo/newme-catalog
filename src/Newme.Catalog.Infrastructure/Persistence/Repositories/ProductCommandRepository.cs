using Newme.Catalog.Domain.Repositories;
using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Infrastructure.Persistence.Repositories
{
    public class ProductCommandRepository : BaseCommandRepository, IProductCommandRepository
    {
        public ProductCommandRepository(CatalogCommandContext productContext) : base(productContext) { }

        public async Task RegisterProductAsync(Product product)
        {
            await context.Products.AddAsync(product);
        }

        public async Task RegisterCategoryAsync(Category category)
        {
            await context.Categories.AddAsync(category);
        }

        public async Task RegisterColorAsync(Color color)
        {
            await context.Colors.AddAsync(color);
        }

        public async Task RegisterSizeAsync(Size size)
        {
            await context.Sizes.AddAsync(size);
        }

        public void Update(Product product)
        {
            context.Products.Update(product);
        }

        public void Delete(ProductId id)
        {
            var product = context.Products.First(x => x.Id == id);
            context.Products.Attach(product);
            context.Products.Remove(product);
        }

        public void UpdateCategory(Category category)
        {
            context.Categories.Update(category);
        }

        public void UpdateColor(Color color)
        {
            context.Colors.Update(color);
        }

        public void UpdateSize(Size size)
        {
            context.Sizes.Update(size);
        }

        #region consulting
        public async Task<T> FindByAsync<T>(Func<T, bool> predicate) where T : class
        {
            return context.Set<T>().FirstOrDefault(predicate);
        }

        public async Task<List<T>> GetByAsync<T>(Func<T, bool> predicate) where T : class
        {
            var response = context.Set<T>().Where(predicate).ToList();
            return await Task.FromResult(response);
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            var response = context.Set<T>().ToList();
            return await Task.FromResult(response);
        }
        #endregion
    }
}