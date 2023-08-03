using MongoDB.Bson.Serialization;
using Newme.Catalog.Application.Consulting.ConsultingModels;

namespace Newme.Catalog.Infrastructure.Consulting.Mappings
{
    public class ColorConsultingModelMapper
    {
        public static void Map()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(ColorConsultingModel)))
            {
                BsonClassMap.RegisterClassMap<ColorConsultingModel>(classMap =>
                {
                    classMap.AutoMap();
                    classMap.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}