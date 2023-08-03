using MediatR;
using Newme.Catalog.Application.ViewModels;

namespace Newme.Catalog.Application.Queries
{
    public class GetCatalogQuery : IRequest<GetCatalogViewModel>
    {
    }
}