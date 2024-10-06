using Dapper;
using Ecart.Models;
using Ecart.Models.Data;
using System.Data;

namespace Ecart.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcartDBContext ecartDbContext;

        public ProductRepository(EcartDBContext ecartDbContext) {
            this.ecartDbContext = ecartDbContext;
        }

        public List<Product> GetProducts()
        {
            using (var conn = ecartDbContext.GetDbConnection())
            {
                var products  =  conn.Query<Product>("Select * from Product").ToList();
                return products;
            }
        }

        public void CreateProduct(Product product)
        {
            var query = "Insert into Product ([ProductNumber],[Description], [Price]) values (@ProductNumber,@Description,@Price)";
            var parameters = new DynamicParameters();
            parameters.Add("ProductNumber", product.ProductNumber,DbType.String);
            parameters.Add("Description", product.Description,DbType.String);
            parameters.Add("Price", product.Price,DbType.Decimal);

            using(var conn = ecartDbContext.GetDbConnection())
            {
                conn.Execute(query, parameters);
            }
        }

        public Product GetProduct(Guid Id)
        {
            string query = "Select * From Product Where Id = @Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", Id);

            using(var conn = ecartDbContext.GetDbConnection())
            {
                Product? product = conn.Query<Product>(query, parameters).FirstOrDefault();
                return product;
            }

        }

        public void UpdateProduct(Product product)
        {
            string query = "Update Product Set ProductNumber = @ProductNumber, Description = @Description, Price = @Price Where ProductNumber = @ProductNumber";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ProductNumber", product.ProductNumber);
            parameters.Add("Description", product.Description);
            parameters.Add("Price", product.Price);

            using (var conn = ecartDbContext.GetDbConnection())
            {
                conn.Execute(query, parameters);
            }

        }

        public void DeleteProduct(Guid Id)
        {
            string query = "Delete From Product Where Id = @Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", Id);

            using (var conn = ecartDbContext.GetDbConnection())
            {
                conn.Execute(query, parameters);
            }
        }
    }
}
