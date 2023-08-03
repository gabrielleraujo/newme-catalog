using MongoDB.Driver;
using Newme.Catalog.Application.Consulting.ConsultingModels;
using Newme.Catalog.Application.Consulting.Repositories;

namespace Newme.Catalog.Infrastructure.Consulting.Repositories
{
    public class GenderQueryRepository : DifferentiatialQueryRepository<GenderConsultingModel>, IGenderQueryRepository
    {
        public GenderQueryRepository(IMongoDatabase context) : base(context, "genders_consulting_model") { }
    }
}