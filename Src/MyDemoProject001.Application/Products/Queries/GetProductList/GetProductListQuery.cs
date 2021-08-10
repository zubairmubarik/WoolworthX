using MediatR;
using MyDemoProject001.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using MyDemoProject001.Application.Common.Models;
using AutoMapper;
using System.Collections.Generic;
using MyDemoProject001.Application.Helpers.Constants;
using MyDemoProject001.Common.Utilities;

namespace MyDemoProject001.Application.Products.Queries.GetProductList
{
    public class GetProductListQuery : IRequest<ResponseDto<List<ProductDto>>>
    {       
    }

    public class GetProductListQueryHandler : IRequestHandler< GetProductListQuery, ResponseDto<List<ProductDto>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductListQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<ProductDto>>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetAsync(cancellationToken);

            _mapper.Map<List<ProductDto>>(result);

            return new ResponseDto<List<ProductDto>>()
            {
                Value = result.Count > 0 ? _mapper.Map<List<ProductDto>>(result) : null,
                Description = (result.Count > 0) ? Messages.RecordsFound : Messages.NoRecordFound,
                ResponseCode = (result.Count > 0) ? ResponseCode.Ok : ResponseCode.NotFound,
                IsError = false
            };          
        }
    }
}