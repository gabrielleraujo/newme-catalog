
using MongoDB.Driver;
using Newme.Catalog.Application.Consulting.ConsultingModels;
using Newme.Catalog.Application.Consulting.Repositories;

namespace Newme.Catalog.Infrastructure.Consulting.Repositories
{
    public class SizeQueryRepository : DifferentiatialQueryRepository<SizeConsultingModel>, ISizeQueryRepository
    {
        public SizeQueryRepository(IMongoDatabase catalogCommandContext) : base(catalogCommandContext, "sizes_consulting_model") { }
    }
}