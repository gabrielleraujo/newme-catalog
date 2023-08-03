using MongoDB.Bson.Serialization;
using Newme.Catalog.Application.Consulting.ConsultingModels;

namespace Newme.Catalog.Infrastructure.Consulting.Mappings
{
    public class CategoryConsultingModelMapper
    {
        public static void Map()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(CategoryConsultingModel)))
            {
                BsonClassMap.RegisterClassMap<CategoryConsultingModel>(classMap =>
                {
                    classMap.AutoMap();
                    classMap.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}