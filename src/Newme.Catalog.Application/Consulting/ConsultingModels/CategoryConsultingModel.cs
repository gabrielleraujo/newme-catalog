using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Application.Consulting.ConsultingModels
{
    public class CategoryConsultingModel : DifferentialConsultingModel
    {
        public CategoryConsultingModel(
            Guid id,
            string name) : base(id, name)
        {
        }

        public static CategoryConsultingModel MapFromDomain(Category entity)
        {
            var result = new CategoryConsultingModel(
                entity.Id.value,
                entity.Name);

            MapFromDomain(result, entity);
            return result;
        }

        public static Category MapToDomain(CategoryConsultingModel consultionModel) 
        {
            var category = new Category(
                new CategoryId(consultionModel.Id),
                consultionModel.Name);

            Entity.SetOriginalInformationWhenMapToDomain(
                category,
                consultionModel.CreateDate,
                consultionModel.LastUpdateDate,
                consultionModel.Active
            );

            return category;
        }
    }
}