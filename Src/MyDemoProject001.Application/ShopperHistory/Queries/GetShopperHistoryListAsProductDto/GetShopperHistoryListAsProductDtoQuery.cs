using MediatR;
using MyDemoProject001.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using MyDemoProject001.Application.Common.Models;
using AutoMapper;
using System.Collections.Generic;
namespace MyDemoProject001.Application.ShopperHistory.Queries.GetShopperHistoryListAsProductDto
{
    public class GetShopperHistoryListAsProductDtoQuery : IRequest<List<ProductDto>>
    {      
    }

    public class GetShopperHistoryListAsProductDtoQueryHandler : IRequestHandler<GetShopperHistoryListAsProductDtoQuery, List<ProductDto>>
    {
        private readonly IShopperHistoryRepository _shopperHistoryRepository;
        private readonly IMapper _mapper;
        public GetShopperHistoryListAsProductDtoQueryHandler(  IShopperHistoryRepository shopperHistoryRepository, IMapper mapper)
        {
            _shopperHistoryRepository = shopperHistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetShopperHistoryListAsProductDtoQuery request, CancellationToken cancellationToken)
        {
            var result = await _shopperHistoryRepository.GetAsync(cancellationToken);            
            var products = new List<ProductDto>();

            //TODO : Imporve this 
            foreach (var s in result)
            {
                products.AddRange(_mapper.Map<List<ProductDto>>(s.Products));
            }

            return products;
        }
    }
}
