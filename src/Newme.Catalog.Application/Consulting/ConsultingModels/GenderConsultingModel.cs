using Newme.Catalog.Domain.Entities;
using Newme.Catalog.Domain.Entities.Gender;

namespace Newme.Catalog.Application.Consulting.ConsultingModels
{
    public class GenderConsultingModel : DifferentialConsultingModel
    {
        public GenderConsultingModel(
            Guid id,
            string name) : base(id, name)
        {
        }

        public static GenderConsultingModel MapFromDomain(Gender entity)
        {
            var consultingModel = new GenderConsultingModel(
                entity.Id.value,
                entity.Name);

            MapFromDomain(consultingModel, entity);
            return consultingModel;
        }

        public static Gender MapToDomain(GenderConsultingModel consultionModel) 
        {
            var entity = new Gender(
                new GenderId(consultionModel.Id),
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