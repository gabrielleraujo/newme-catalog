using MongoDB.Bson.Serialization;
using Newme.Catalog.Application.Consulting.ConsultingModels;

namespace Newme.Catalog.Infrastructure.Consulting.Mappings
{
    public class ProductConsultingModelMapper
    {
        public static void Map()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(ProductConsultingModel)))
            {
                BsonClassMap.RegisterClassMap<ProductConsultingModel>(classMap =>
                {
                    classMap.AutoMap();
                    classMap.SetIgnoreExtraElements(true);
                    classMap.MapMember(p => p.Sku).SetElementName("sku");
                    classMap.MapMember(p => p.Currency).SetElementName("price_currency");
                    classMap.MapMember(p => p.Amount).SetElementName("price_amount");
                    classMap.MapMember(p => p.Name).SetElementName("name");
                    classMap.MapMember(p => p.Description).SetElementName("description");
                    classMap.MapMember(p => p.Gender).SetElementName("gender");
                    classMap.MapMember(p => p.Category).SetElementName("category");
                    classMap.MapMember(p => p.Color).SetElementName("color");
                    classMap.MapMember(p => p.Size).SetElementName("size");
                    classMap.MapMember(p => p.Stock).SetElementName("stock");
                    classMap.MapMember(p => p.Promotion).SetElementName("promotion");
                    classMap.MapMember(p => p.Images).SetElementName("images");                    
                });
            }
        }
    }
}
