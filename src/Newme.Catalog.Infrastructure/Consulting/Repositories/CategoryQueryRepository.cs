using MongoDB.Driver;
using Newme.Catalog.Application.Consulting.ConsultingModels;
using Newme.Catalog.Application.Consulting.Repositories;

namespace Newme.Catalog.Infrastructure.Consulting.Repositories
{
    public class CategoryQueryRepository : DifferentiatialQueryRepository<CategoryConsultingModel>, ICategoryQueryRepository
    {
        public CategoryQueryRepository(IMongoDatabase catalogContext) : base(catalogContext, "categories_consulting_model") { }
    }
}