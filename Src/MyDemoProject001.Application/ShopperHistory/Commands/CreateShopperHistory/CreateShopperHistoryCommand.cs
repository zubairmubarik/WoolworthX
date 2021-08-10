using MediatR;
using MyDemoProject001.Domain.Entities;
using MyDemoProject001.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;
using MyDemoProject001.Application.Common.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace MyDemoProject001.Application.ShopperHistory.Commands.CreateShopperHistory
{
    public class CreateShopperHistoryCommand : IRequest<ShopperHistoryDto>
    {
        private readonly ShopperHistoryDocument _shopperHistory;
        public ShopperHistoryDocument ShopperHistory { get { return _shopperHistory; } }
        public CreateShopperHistoryCommand(ShopperHistoryDocument shopperHistory)
        {
            _shopperHistory = shopperHistory;
        }
    }

    public class CreateShopperHistoryCommandHandler : IRequestHandler<CreateShopperHistoryCommand, ShopperHistoryDto>
    {
        private readonly IShopperHistoryRepository _shopperHistoryRepository;
        private readonly IMapper _mapper;        
        private readonly ILogger _logger;
        public readonly IMongoContext _context;

        public CreateShopperHistoryCommandHandler(IShopperHistoryRepository shopperHistoryRepository, IMapper mapper, ILogger<CreateShopperHistoryCommandHandler> logger)
        {
            _shopperHistoryRepository = shopperHistoryRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ShopperHistoryDto> Handle(CreateShopperHistoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executing ShopperHistory Create Handler");
          
            var result = await _shopperHistoryRepository.CreateAsync(request.ShopperHistory, cancellationToken);

            return result != null ? _mapper.Map<ShopperHistoryDto>(result) : null;
        }
    }
}
