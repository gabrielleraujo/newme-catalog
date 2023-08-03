using MediatR;
using Newme.Catalog.Application.Consulting.Repositories;
using Newme.Catalog.Application.ViewModels;

namespace Newme.Catalog.Application.Queries
{
    public class SearchByNameQueryHandler : IRequestHandler<SearchByNameQuery, GetCatalogViewModel>
    {
        private readonly IProductQueryRepository _repositoryQuery;

        public SearchByNameQueryHandler(IProductQueryRepository repositoryQuery)
        {
            _repositoryQuery = repositoryQuery;
        }

        public async Task<GetCatalogViewModel> Handle(SearchByNameQuery command, CancellationToken cancellationToken)
        {
            var products = await _repositoryQuery.SearchByNameAsync(command.Name);
            
            return GetCatalogViewModelFactory.Build(
                products
            );
        }
    }
}
