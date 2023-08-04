using System.Linq.Expressions;
using MongoDB.Driver;
using Newme.Catalog.Application.Consulting.ConsultingModels;
using Newme.Catalog.Application.Consulting.Repositories;

namespace Newme.Catalog.Infrastructure.Consulting.Repositories
{
    public class BaseQueryRepository<T> : IBaseQueryRepository<T>
        where T : ConsultingModel
    {
        protected readonly IMongoCollection<T> _collection;
        public BaseQueryRepository(IMongoDatabase database, String collection)
        {
            _collection = database.GetCollection<T>(collection);
        }
        
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(c => true).ToListAsync();
        }
        
        public async Task<T> GetByIdAsync(Guid id)
        {
            var result = await _collection.FindAsync(x => x.Id == id);
            return result.SingleOrDefault();        
        }

        public async Task<IList<T>> GetByFilterAsync(Expression<Func<T, bool>> expression)
        {
            var filter = Builders<T>.Filter
                .Where(expression);

            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            var result = await GetByIdAsync(id);
            return result == null ? false : true;
        }

        public async Task<IEnumerable<T>> GetAllByAsync<TValueMatch>(
            TValueMatch value, Expression<Func<T, TValueMatch>> expression)
        {
            var filter = Builders<T>.Filter
                .Eq(expression, value);

            var result = await _collection.FindAsync(filter);

            return result.ToList();
        }

        public async Task<bool> ExistsByAsync <TValueMatch>(
            TValueMatch value, Expression<Func<T, TValueMatch>> expression)
        {
            var result = await GetAllByAsync(value, expression);
            return result == null ? false : true;
        }

        public async Task AddAsync(T value) 
        {
            await _collection.InsertOneAsync(value);
        }

        public async Task UpdateAsync<TValueUpdate>(
            Guid id, TValueUpdate newValue, Expression<Func<T, TValueUpdate>> expression) 
        {
            var filter = Builders<T>.Filter
                .Eq(model => model.Id, id);
                
            var update = Builders<T>.Update
                .Set<TValueUpdate>(expression, newValue)
                .Set(x => x.LastUpdateDate, DateTime.Now);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAsync(Guid id) 
        {
            var filter = Builders<T>.Filter
                .Eq(model => model.Id, id);

            await _collection.DeleteManyAsync(filter);
        }

        public async Task ActivateAsync(Guid id) 
        {
            await UpdateStateAsync(id, true);      
        }

        public async Task DeactivateAsync(Guid id) 
        {
            await UpdateStateAsync(id, false);        
        }

        private async Task UpdateStateAsync(Guid id, bool state) 
        {
            var filter = Builders<T>.Filter
                .Eq(model => model.Id, id);

            var update = Builders<T>.Update
                .Set(x => x.Active, state);

            await _collection.UpdateOneAsync(filter, update);        
        }
    }
}
