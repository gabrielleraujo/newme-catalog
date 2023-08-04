using MediatR;
using Newme.Catalog.Application.ViewModels;

namespace Newme.Catalog.Application.Queries
{
    public class GetCatalogByFilterQuery : IRequest<GetCatalogViewModel>
    {
        public GetCatalogByFilterQuery(
            IList<string> categories, 
            IList<string> colors, 
            IList<string> sizes, 
            IList<string> genders, 
            decimal minPrice, 
            decimal maxPrice)
        {
            Categories = categories;
            Colors = colors;
            Sizes = sizes;
            Genders = genders;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }

        public IList<string> Categories { get; set; } = new List<string>();
        public IList<string> Colors { get; set; } = new List<string>();
        public IList<string> Sizes { get; set; } = new List<string>();
        public IList<string> Genders { get; set; } = new List<string>();
        public decimal MinPrice { get; set; } = 0.0m;
        public decimal MaxPrice { get; set; } = decimal.MaxValue;
    }
}