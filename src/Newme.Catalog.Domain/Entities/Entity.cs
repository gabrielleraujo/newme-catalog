namespace Newme.Catalog.Domain.Entities
{
    public abstract class Entity
    {
        public DateTime CreateDate { get; private set; }
        public DateTime? LastUpdateDate { get; protected set; }
        public bool Active { get; protected set; }

        protected Entity()
        {
            CreateDate = DateTime.Now;
            LastUpdateDate = null;
            Active = true;
        }

        public static void SetOriginalInformationWhenMapToDomain(
            Entity entity,
            DateTime createDate,
            DateTime? lastUpdateDate,
            bool active)
        {
            entity.CreateDate = createDate;
            entity.LastUpdateDate = lastUpdateDate;
            entity.Active = active;
        }
        
        public abstract void Activate();
        public abstract void Deactivate();
    }
}