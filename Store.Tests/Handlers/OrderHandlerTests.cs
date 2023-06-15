using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Handlers;
using Store.Domain.Repositories.Interfaces;
using Store.Tests.Repositories.Mocks;

namespace Store.Tests.Handlers;

[TestClass]
public class OrderHandlerTests
{
    private readonly ICustumerRepository _custumerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderHandlerTests()
    {
        _custumerRepository = new FakeCustumerRepository();
        _deliveryFeeRepository = new FakeDeliveryFeeRepository();
        _discountRepository = new FakeDiscountRepository();
        _productRepository = new FakeProductRepository();
        _orderRepository = new FakeOrderRepository();
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Given_An_Invalid_Client_Order_Should_Not_Be_Created()
    {
        var command = new CreateOrderCommand();
        command.Custumer = "12345678";
        command.Zipcode = "1111111";
        command.PromoCode = "1111111";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        var handler = new OrderHandler(_custumerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);

        handler.Handle(command);

        Assert.AreEqual(!handler.IsValid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Given_An_Invalid_Cep_Order_Should_Be_Created()
    {
        var command = new CreateOrderCommand();
        command.Custumer = "12345678";
        command.Zipcode = "1111111";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        Assert.AreEqual(command.IsValid, true);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Given_An_Invalid_PromoCode_Order_Should_Be_Created()
    {
        var command = new CreateOrderCommand();
        command.Custumer = "12345678";
        command.Zipcode = "12345678";
        command.PromoCode = "1111111";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        Assert.AreEqual(command.IsValid, true);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Given_An_Empty_Order_Should_Not_Be_Created()
    {
        var command = new CreateOrderCommand();
        command.Items.Add(new CreateOrderItemCommand());
        command.Items.Add(new CreateOrderItemCommand());

        Assert.AreEqual(!command.IsValid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Given_An_Invalid_Command_Order_Should_Not_Be_Created()
    {
        var command = new CreateOrderCommand();
        command.Custumer = "";
        command.Zipcode = "12345678";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        Assert.AreEqual(!command.IsValid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Given_An_Valid_Command_Order_Should_Be_Created()
    {
        var command = new CreateOrderCommand();
        command.Custumer = "12345678";
        command.Zipcode = "12345678";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        var handler = new OrderHandler(_custumerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);

        handler.Handle(command);

        Assert.AreEqual(handler.IsValid, true);
    }

}