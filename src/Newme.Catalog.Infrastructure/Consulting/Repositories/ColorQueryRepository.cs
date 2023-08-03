using MongoDB.Driver;
using Newme.Catalog.Application.Consulting.ConsultingModels;
using Newme.Catalog.Application.Consulting.Repositories;

namespace Newme.Catalog.Infrastructure.Consulting.Repositories
{
    public class ColorQueryRepository : DifferentiatialQueryRepository<ColorConsultingModel>, IColorQueryRepository
    {
        public ColorQueryRepository(IMongoDatabase catalogContext) : base(catalogContext, "colors_consulting_model") { }
    }
}