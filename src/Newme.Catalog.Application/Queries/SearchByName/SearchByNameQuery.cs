using MediatR;
using Newme.Catalog.Application.ViewModels;

namespace Newme.Catalog.Application.Queries
{
    public class SearchByNameQuery : IRequest<GetCatalogViewModel>
    {
        public SearchByNameQuery(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}