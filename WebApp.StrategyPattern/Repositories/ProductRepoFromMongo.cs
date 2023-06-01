using MongoDB.Driver;
using WebApp.StrategyPattern.Models;

namespace WebApp.StrategyPattern.Repositories
{
    public class ProductRepoFromMongo : IProductRepo
    {
        private readonly IMongoCollection<Product> _mongoCollection;

        public ProductRepoFromMongo(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("ProductDb");
            _mongoCollection = db.GetCollection<Product>("Products");
        }

        public async Task<Product> Add(Product product)
        {
            await _mongoCollection.InsertOneAsync(product);
            return product;
        }

        public async Task Delete(Product product)
        {
            await _mongoCollection.DeleteOneAsync(x=>x.Id == product.Id);
        }

        public async Task<Product> GetById(string id)
        {
            return await _mongoCollection.Find(x=>x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetByUserId(string userId)
        {
            return await _mongoCollection.Find(x=>x.UserId == userId).ToListAsync();
        }

        public async Task Update(Product product)
        {
            await _mongoCollection.FindOneAndReplaceAsync(x => x.Id == product.Id, product);
        }
    }
}
