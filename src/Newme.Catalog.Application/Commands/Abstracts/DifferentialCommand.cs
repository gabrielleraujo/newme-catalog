namespace Newme.Catalog.Application.Commands
{
    public abstract class DifferentialCommand : Command
    {
        public string Name { get; protected set; }
    }
}