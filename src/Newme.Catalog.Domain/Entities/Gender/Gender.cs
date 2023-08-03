namespace Newme.Catalog.Domain.Entities.Gender
{
    public class Gender : Differential
    {
        private Gender() {}
        public Gender(GenderId id, string name) : base(name)
        {
            Id = id;
        }

        public GenderId Id { get; private set; }
        public override void FixName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException($"Invalid gender name {name}.");
            Name = name;
        }
    }
}