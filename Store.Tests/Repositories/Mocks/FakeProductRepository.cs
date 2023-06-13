using Store.Domain.Entities;
using Store.Domain.Repositories.Interfaces;

namespace Store.Tests.Repositories.Mocks;

public class FakeProductRepository : IProductRepository
{
    public IEnumerable<Product> Get(IEnumerable<Guid> guids)
    {
        IList<Product> products = new List<Product>();
        products.Add(new Product("Produto 0x01", 10, true));
        products.Add(new Product("Produto 0x02", 10, true));
        products.Add(new Product("Produto 0x03", 10, true));
        products.Add(new Product("Produto 0x04", 10, false));
        products.Add(new Product("Produto 0x05", 10, false));

        return products;
    }
}
