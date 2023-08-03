using MongoDB.Bson.Serialization;
using Newme.Catalog.Application.Consulting.ConsultingModels;

namespace Newme.Catalog.Infrastructure.Consulting.Mappings
{
    public class PromotionConsultingModelMapper
    {
        public static void Map()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(PromotionConsultingModel)))
            {
                BsonClassMap.RegisterClassMap<PromotionConsultingModel>(classMap =>
                {
                    classMap.AutoMap();
                    classMap.SetIgnoreExtraElements(true);
                    classMap.MapMember(p => p.ProductId).SetElementName("product_id");
                    classMap.MapMember(p => p.Name).SetElementName("name");
                    classMap.MapMember(p => p.Start).SetElementName("start");
                    classMap.MapMember(p => p.End).SetElementName("end");
                    classMap.MapMember(p => p.Percentage).SetElementName("discount_percentage");
                    classMap.MapMember(p => p.Currency).SetElementName("discount_currency");
                    classMap.MapMember(p => p.Amount).SetElementName("discount_amount");
                    classMap.MapMember(p => p.Description).SetElementName("description");        
                });
            }
        }
    }
}
