using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Application.Consulting.ConsultingModels
{
    public class ColorConsultingModel : DifferentialConsultingModel
    {
        public ColorConsultingModel(
            Guid id,
            string name) : base(id, name)
        {
        }

        public static ColorConsultingModel MapFromDomain(Color entity)
        {
            var consultingModel = new ColorConsultingModel(
                entity.Id.value,
                entity.Name);

            MapFromDomain(consultingModel, entity);
            return consultingModel;
        }

        public static Color MapToDomain(ColorConsultingModel consultionModel) 
        {
            var entity = new Color(
                new ColorId(consultionModel.Id),
                consultionModel.Name);

            Entity.SetOriginalInformationWhenMapToDomain(
                entity,
                consultionModel.CreateDate,
                consultionModel.LastUpdateDate,
                consultionModel.Active
            );

            return entity;
        }
    }
}