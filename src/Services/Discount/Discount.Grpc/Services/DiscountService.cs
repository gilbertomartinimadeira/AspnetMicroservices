using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;
    
        public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        } 

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {        
            var coupon = await  _repository.GetDiscount(request.ProductName);

            if(coupon == null) throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product name = {request.ProductName} was not found"));

            return new CouponModel { 
                Amount = coupon.Amount,
                Description = coupon.Description, 
                ProductName = coupon.ProductName
            };
        }
            

    }
}