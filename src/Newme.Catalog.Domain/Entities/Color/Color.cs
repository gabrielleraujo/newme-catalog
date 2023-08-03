namespace Newme.Catalog.Domain.Entities
{
    public class Color : Differential
    {
        private Color() {}
        public Color(ColorId id, string name) : base(name)
        {
            Id = id;
        }

        public ColorId Id { get; private set; }

        public override void FixName(string name)
        {
            if (name.Length < 4) throw new ArgumentException($"Invalid color name {name}.");
            Name = name;
        }
    }
}