using MongoDB.Bson.Serialization;
using Newme.Catalog.Application.Consulting.ConsultingModels;

namespace Newme.Catalog.Infrastructure.Consulting.Mappings
{
    public class ConsultingModelMapper
    {
        public static void Map()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(ConsultingModel)))
            {
                BsonClassMap.RegisterClassMap<ConsultingModel>(classMap =>
                {
                    classMap.AutoMap();
                    classMap.SetIgnoreExtraElements(true);
                    classMap.MapIdField(p => p.Id);
                    classMap.MapMember(p => p.CreateDate).SetElementName("create_date");
                    classMap.MapMember(p => p.LastUpdateDate).SetElementName("last_update_date");
                    classMap.MapMember(p => p.Active).SetElementName("is_active");
                });
            }
        }
    }
}
