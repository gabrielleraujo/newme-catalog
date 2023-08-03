namespace Newme.Catalog.Infrastructure.Consulting.Mappings
{
    public class MongoMapper
    {
        public static void Configure()
        {
            ConsultingModelMapper.Map();
            ProductConsultingModelMapper.Map();
            PromotionConsultingModelMapper.Map();
            DifferentialConsultingModelMapper.Map();
            GenderConsultingModelMapper.Map();
            CategoryConsultingModelMapper.Map();
            ColorConsultingModelMapper.Map();
            SizeConsultingModelMapper.Map();
        }
    }
}