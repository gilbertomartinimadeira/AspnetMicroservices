using Discount.Grpc.Entities;
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
            var coupon = await _repository.GetDiscount(request.ProductName);

            if (coupon == null) throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product name = {request.ProductName} was not found"));

            return new CouponModel
            {
                Amount = coupon.Amount,
                Description = coupon.Description,
                ProductName = coupon.ProductName
            };
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var couponEntity = new Coupon()
            {
                Amount = request.Coupon.Amount,
                ProductName = request.Coupon.ProductName,
                Description = request.Coupon.Description
            };

            var couponCreated = await _repository.CreateDiscount(couponEntity);

            if (couponCreated)
            {
                return new CouponModel
                {
                    Amount = request.Coupon.Amount,
                    Description = request.Coupon.Description,
                    ProductName = request.Coupon.ProductName
                };
            }

            return new CouponModel
            {
                Amount = 0,
                Description = "Couldn't create coupon",
                ProductName = "N/A"
            };
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var couponEntity = new Coupon()
            {
                Amount = request.Coupon.Amount,
                ProductName = request.Coupon.ProductName,
                Description = request.Coupon.Description
            };

            var couponUpdated = await _repository.UpdateDiscount(couponEntity);

            if (couponUpdated)
            {
                return new CouponModel
                {
                    Amount = request.Coupon.Amount,
                    Description = request.Coupon.Description,
                    ProductName = request.Coupon.ProductName
                };
            }

            return new CouponModel
            {
                Amount = 0,
                Description = "Couldn't update coupon",
                ProductName = "N/A"
            };
        }
    
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _repository.DeleteDiscount(request.ProductName);

            var response = new DeleteDiscountResponse {
                Success = deleted
            };

            return response;

        }



    }
}