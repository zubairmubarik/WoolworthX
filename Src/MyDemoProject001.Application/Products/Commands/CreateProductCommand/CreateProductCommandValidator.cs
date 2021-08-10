using MediatR;
using MyDemoProject001.Domain.Entities;

using System.Threading;
using System.Threading.Tasks;
namespace MyDemoProject001.Application.Products.Commands.CreateProductCommand
{
    public class CreateProductCommandValidator : IRequest<bool>
    {
        private readonly ProductDocument _product;
        public ProductDocument Product { get { return _product; } }
        public CreateProductCommandValidator(ProductDocument product)
        {
            _product = product;
        }
    }

    public class CreateProductCommandValidatorHandler : IRequestHandler<CreateProductCommandValidator, bool>
    {
        public async Task<bool> Handle(CreateProductCommandValidator request, CancellationToken cancellationToken)
        {
            await Task.Delay(500);
            return true;
        }
    }
}
