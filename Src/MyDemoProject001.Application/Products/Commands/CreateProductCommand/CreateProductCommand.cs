using MediatR;
using MyDemoProject001.Domain.Entities;
using MyDemoProject001.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;
using MyDemoProject001.Application.Common.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace MyDemoProject001.Application.Products.Commands.CreateProductCommand
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        private readonly ProductDocument _product;
        public ProductDocument Product { get { return _product; } }
        public CreateProductCommand(ProductDocument product)
        {
            _product = product;
        }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public readonly IMongoContext _context;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IMediator mediator,IMongoContext context, ILogger<CreateProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
            _context = context;
        }
        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executing Product Create Handler");
            var isValid = await _mediator.Send(new CreateProductCommandValidator(request.Product));

            // // Add our new user     
            // _productRepository.AddProduct(request.Product, cancellationToken);

            // // If everything is ok
            //var result = await _context.CommitChangesAsync();

            var result = isValid ? await _productRepository.CreateAsync(request.Product, cancellationToken) : null;

            return result != null ? _mapper.Map<ProductDto>(result) : null;

        }
    }
}
