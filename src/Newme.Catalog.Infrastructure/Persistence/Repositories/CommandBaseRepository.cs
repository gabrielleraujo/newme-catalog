namespace Newme.Catalog.Infrastructure.Persistence.Repositories
{
    public abstract class BaseCommandRepository : IDisposable
    {
        protected CatalogCommandContext context;

        public BaseCommandRepository(CatalogCommandContext productContext)
        {
            context = productContext;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<bool> Commit()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}