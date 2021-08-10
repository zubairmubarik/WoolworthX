using MediatR;
using MyDemoProject001.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using MyDemoProject001.Application.Common.Models;
using AutoMapper;
using System.Collections.Generic;

namespace MyDemoProject001.Application.ShopperHistory.Queries.GetShopperList
{
    public class GetShopperHistoryListQuery : IRequest<List<ShopperHistoryDto>>
    {
    }

    public class GetShopperHistoryListQueryHandler : IRequestHandler<GetShopperHistoryListQuery, List<ShopperHistoryDto>>
    {
        private readonly IShopperHistoryRepository _shopperHistoryRepository;
        private readonly IMapper _mapper;
        public GetShopperHistoryListQueryHandler(IShopperHistoryRepository shopperHistoryRepository, IMapper mapper)
        {
            _shopperHistoryRepository = shopperHistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<ShopperHistoryDto>> Handle(GetShopperHistoryListQuery request, CancellationToken cancellationToken)
        {
            var result = await _shopperHistoryRepository.GetAsync(cancellationToken);

            return _mapper.Map<List<ShopperHistoryDto>>(result);
        }
    }
}
