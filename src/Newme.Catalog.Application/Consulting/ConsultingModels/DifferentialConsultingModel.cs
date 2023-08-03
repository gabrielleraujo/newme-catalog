namespace Newme.Catalog.Application.Consulting.ConsultingModels
{
    public abstract class DifferentialConsultingModel : ConsultingModel
    {
        protected DifferentialConsultingModel(
            Guid id,
            string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}