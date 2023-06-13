using Store.Domain.Entities;
using Store.Domain.Queries;

namespace Store.Tests.Queries;

[TestClass]
public class ProductQueriesTests
{
    private IList<Product> _products;

    public ProductQueriesTests()
    {
        _products = new List<Product>();
        _products.Add(new Product("Produto 0x01", 10, true));
        _products.Add(new Product("Produto 0x02", 20, true));
        _products.Add(new Product("Produto 0x03", 30, true));
        _products.Add(new Product("Produto 0x04", 40, false));
        _products.Add(new Product("Produto 0x05", 50, false));
    }

    [TestMethod]
    [TestCategory("Queries")]
    public void Given_Active_Products_Query_Return_3()
    {
        var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
        Assert.AreEqual(result.Count(), 3);
    }

    [TestMethod]
    [TestCategory("Queries")]
    public void Given_Inactive_Products_Query_Return_2()
    {
        var result = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
        Assert.AreEqual(result.Count(), 2);
    }
}