using Newme.Catalog.Domain.Entities.Gender;

namespace Newme.Catalog.Domain.Entities
{
    public record DifferentialGroup(
        CategoryId category, 
        ColorId color, 
        SizeId size,
        GenderId gender);
}
