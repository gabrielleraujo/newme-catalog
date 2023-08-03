using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Application.Consulting.ConsultingModels
{
    public class SizeConsultingModel : DifferentialConsultingModel
    {
        public SizeConsultingModel(
            Guid id,
            string name) : base(id, name)
        {
        }

        public static SizeConsultingModel MapFromDomain(Size entity)
        {
            var consultingModel = new SizeConsultingModel(
                entity.Id.value,
                entity.Name);

            MapFromDomain(consultingModel, entity);
            return consultingModel;
        }

        public static Size MapToDomain(SizeConsultingModel consultionModel) 
        {
            var entity = new Size(
                 new SizeId(consultionModel.Id),
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