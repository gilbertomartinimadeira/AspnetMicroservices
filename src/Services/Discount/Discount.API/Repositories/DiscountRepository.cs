using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(
                _configuration["DatabaseSettings:ConnectionString"]);

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM COUPON WHERE ProductName = @ProductName",
                    new { ProductName = productName });

            if(coupon == null){
                return new Coupon{
                    Description = "No Discount Desc",
                    Amount= 0, 
                    ProductName = "No Discount" 
                    };
            }else{
                return coupon;
            }
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(
                _configuration["DatabaseSettings:ConnectionString"]);

            var affected =  await connection.ExecuteAsync
            ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES ( @ProductName, @Description, @Amount)",
            coupon);

            return affected > 0;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(
                _configuration["DatabaseSettings:ConnectionString"]);

            var affected =  await connection.ExecuteAsync
            (@"UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount
            WHERE Id = @Id ", coupon);

            return affected > 0;
        }
        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(
                _configuration["DatabaseSettings:ConnectionString"]);

            var affected =  await connection.ExecuteAsync
            (@"DELETE FROM Coupon WHERE ProductName = @ProductName", new {ProductName = productName});

            return affected > 0;
        }
    }
}