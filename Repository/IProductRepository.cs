using Ecart.Models;

namespace Ecart.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetProducts();

        public void CreateProduct(Product product);

        public Product GetProduct(Guid Id);

        public void UpdateProduct(Product product);

        public void DeleteProduct(Guid Id);
    }
}
