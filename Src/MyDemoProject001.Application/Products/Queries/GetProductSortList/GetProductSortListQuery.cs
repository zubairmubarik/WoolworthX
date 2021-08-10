using MediatR;
using MyDemoProject001.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using MyDemoProject001.Application.Common.Models;
using AutoMapper;
using System.Collections.Generic;
using MyDemoProject001.Domain.Entities;
using MongoDB.Driver;
using MyDemoProject001.Application.Common.Logic;
using MyDemoProject001.Application.ShopperHistory.Queries.GetShopperHistoryListAsProductDto;
using System.Linq;

namespace MyDemoProject001.Application.Products.Queries.GetProductSortList
{
    public class GetProductSortListQuery : IRequest<List<ProductDto>>
    {
        private readonly string _sortOption;
        public string SortOption { get { return _sortOption; } }

        public GetProductSortListQuery(string sortOption)
        {
            _sortOption = sortOption;
        }

        public class GetProductSortListQueryHandler : IRequestHandler<GetProductSortListQuery, List<ProductDto>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            public GetProductSortListQueryHandler(IProductRepository productRepository, IMediator mediator, IMapper mapper)
            {
                _productRepository = productRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            public async Task<List<ProductDto>> Handle(GetProductSortListQuery request, CancellationToken cancellationToken)
            {
                ISortingStrategy sortingStrategy = null;
                switch (request.SortOption)
                {
                    case "Low":
                    case "low":
                        sortingStrategy = new SortByLowToHighPriceStrategy();
                        break;
                    case "High":
                    case "high":
                        sortingStrategy = new SortByHighToLowPriceStrategy();
                        break;
                    case "Ascending":
                    case "ascending":
                        sortingStrategy = new SortByAscendingNameStrategy();
                        break;
                    case "Descending":
                    case "descending":
                        sortingStrategy = new SortByDescendingName();
                        break;
                    case "Recommended":
                    case "recommended":
                        sortingStrategy = new SortByPopularityStrategy();
                        break;
                    default:
                        sortingStrategy = new SortByAscendingNameStrategy();
                        break;
                }

                // Build Command builder
                var options = new FindOptions<ProductDocument>()
                {
                    Sort = sortingStrategy.Sort()
                };

                var customerOrder = _mapper.Map<List<ProductDto>>(await _productRepository.GetSortedListAsync(options, cancellationToken));

                if (request.SortOption == "Recommended" || request.SortOption == "recommended")
                {  
                    var orderHistory = await _mediator.Send(new GetShopperHistoryListAsProductDtoQuery());
                                       
                    orderHistory = orderHistory
                           .GroupBy(l => l.Name)
                           .Select(cl => new ProductDto
                           {
                               Name = cl.First().Name,
                               Quantity = cl.Sum(c=>c.Quantity),
                               Price = cl.First().Price,
                           }).OrderByDescending(x => x.Quantity).ToList();
                   
                    //TODO Put in extension method
                    orderHistory.AddRange(customerOrder.Where(p => orderHistory.All(p2 => p2.Name != p.Name)));
                    
                    return orderHistory;
                }

                var result = await _productRepository.GetSortedListAsync(options, cancellationToken);

                return _mapper.Map<List<ProductDto>>(result);
            }




        }
    }
}

