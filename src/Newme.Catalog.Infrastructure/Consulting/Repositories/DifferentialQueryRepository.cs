using MongoDB.Driver;
using Newme.Catalog.Application.Consulting.ConsultingModels;
using Newme.Catalog.Application.Consulting.Repositories;

namespace Newme.Catalog.Infrastructure.Consulting.Repositories
{
    public class DifferentiatialQueryRepository<T> : BaseQueryRepository<T>, IDifferentiatialQueryRepository<T>
        where T : DifferentialConsultingModel
    {
        public DifferentiatialQueryRepository(IMongoDatabase catalogContext, string collection) : base(catalogContext, collection) { }
    
        public async Task<T> GetByNameAsync(string name)
        {
            var result = await _collection.FindAsync(c => c.Name.Equals(name));
            return result.FirstOrDefault();
        }

        public async Task<bool> ExistsAsync(string value)
        {
            var result = await _collection.FindAsync(x => x.Name.Equals(value));
            return result.Any();
        }

        public async Task<IEnumerable<T>> SearchByNameAsync(string name)
        {
            var result = await _collection.FindAsync(x => x.Name.ToString().Contains(name));
            return result.ToList();
        }

        public async Task UpdateManyAsync(Guid id, T newModel) 
        {
            var filter = Builders<T>.Filter
                .Eq(model => model.Id, id);
                
            var update = Builders<T>.Update
                .Set(x => x.Name, newModel.Name)
                .Set(x => x.Active, newModel.Active)
                .Set(x => x.LastUpdateDate, DateTime.Now);

            await _collection.UpdateManyAsync(filter, update);
        }
    }
}