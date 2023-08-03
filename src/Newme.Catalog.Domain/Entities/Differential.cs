namespace Newme.Catalog.Domain.Entities
{
    public abstract class Differential : Entity
    {
        protected Differential() {}

        protected Differential(string name)
        {
            Name = name;
        }

        public string Name { get; protected set; }
        public override void Activate()
        {
            Active = true;
            LastUpdateDate = DateTime.Now;
        }

        public override void Deactivate()
        {
            Active = false;
            LastUpdateDate = DateTime.Now;
        }

        public abstract void FixName(string name);
    }
}
