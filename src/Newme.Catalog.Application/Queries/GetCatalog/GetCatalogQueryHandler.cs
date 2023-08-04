using MediatR;
using Newme.Catalog.Application.Consulting.Repositories;
using Newme.Catalog.Application.ViewModels;

namespace Newme.Catalog.Application.Queries
{
    public class GetCatalogQueryHandler : IRequestHandler<GetCatalogQuery, GetCatalogViewModel>
    {
        private readonly IProductQueryRepository _repositoryQuery;

        public GetCatalogQueryHandler(IProductQueryRepository repositoryQuery)
        {
            _repositoryQuery = repositoryQuery;
        }

        public async Task<GetCatalogViewModel> Handle(GetCatalogQuery query, CancellationToken cancellationToken)
        {
            var products = await _repositoryQuery.GetAllAsync();

            return GetCatalogViewModelFactory.Build(
                products
            );
        }
    }
}
