using MongoDB.Driver;
using Newme.Catalog.Application.Consulting.ConsultingModels;
using Newme.Catalog.Application.Consulting.Repositories;

namespace Newme.Catalog.Infrastructure.Consulting.Repositories
{
    public class ProductQueryRepository : BaseQueryRepository<ProductConsultingModel>, IProductQueryRepository
    {
        public ProductQueryRepository(IMongoDatabase catalogContext) : base(catalogContext, "products_consulting_model") { }

        public async Task AddImagesAsync(Guid id, IList<string> images)
        {
            var filter = Builders<ProductConsultingModel>.Filter
                .Eq(model => model.Id, id);

            var update = Builders<ProductConsultingModel>.Update
                .AddToSetEach(x => x.Images, images);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task<ProductConsultingModel> GetByNameAsync(string name)
        {
            var result = await _collection.FindAsync(c => c.Name == name);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<ProductConsultingModel>> SearchByNameAsync(string name)
        {
            var result = await _collection.FindAsync(x => x.Name.ToString().Contains(name));
            return result.ToList();
        }

        public async Task UpdateManyAsync(Guid id, ProductConsultingModel newModel) 
        {
            var filter = Builders<ProductConsultingModel>.Filter
                .Eq(model => model.Id, id);
                
            var update = Builders<ProductConsultingModel>.Update
                .Set(x => x.Name, newModel.Name)
                .Set(x => x.Active, newModel.Active)
                .Set(x => x.LastUpdateDate, DateTime.Now)
                .Set(x => x.Category, newModel.Category)
                .Set(x => x.Color, newModel.Color)
                .Set(x => x.Size, newModel.Size)
                .Set(x => x.Currency, newModel.Currency)
                .Set(x => x.Amount, newModel.Amount)
                .Set(x => x.Stock, newModel.Stock)
                .Set(x => x.Promotion, newModel.Promotion)
                .Set(x => x.LastUpdateDate, DateTime.Now)
                .Set(x => x.Images, newModel.Images);

            await _collection.UpdateManyAsync(filter, update);
        }
    }
}