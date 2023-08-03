
using Newme.Catalog.Domain.Entities;

namespace Newme.Catalog.Application.Consulting.ConsultingModels
{
    public abstract class ConsultingModel
    {
        protected ConsultingModel(Guid id)
        {
            Id = id;
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            Active = true;
        }

        public Guid Id { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime? LastUpdateDate { get; private set; }
        public bool Active { get; private set; }

        protected static void MapFromDomain(ConsultingModel consultingModel, Entity entity)
        {
            consultingModel.CreateDate = entity.CreateDate;
            consultingModel.LastUpdateDate = entity.LastUpdateDate;
            consultingModel.Active = entity.Active;
        }
    }
}