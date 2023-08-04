using MediatR;
using Newme.Catalog.Application.Consulting.ConsultingModels;
using Newme.Catalog.Application.Consulting.Repositories;
using Newme.Catalog.Application.ViewModels;

namespace Newme.Catalog.Application.Queries
{
    public class GetCatalogByFilterQueryHandler : IRequestHandler<GetCatalogByFilterQuery, GetCatalogViewModel>
    {
        private readonly IProductQueryRepository _repositoryQuery;

        public GetCatalogByFilterQueryHandler(IProductQueryRepository repositoryQuery)
        {
            _repositoryQuery = repositoryQuery;
        }

        public async Task<GetCatalogViewModel> Handle(GetCatalogByFilterQuery query, CancellationToken cancellationToken)
        {
            var products = await _repositoryQuery.GetByFilterAsync(
                model => 
                    model.Amount <= query.MaxPrice 
                    && model.Amount >= query.MinPrice
                    && query.Categories.Contains(model.Category.Name)
                    && query.Colors.Contains(model.Color.Name)
                    && query.Sizes.Contains(model.Size.Name)
                    && query.Genders.Contains(model.Gender.Name)
            );

            return GetCatalogViewModelFactory.Build(
                products
            );
        }
    }
}
