using Store.Domain.Entities;

namespace Store.Tests.Entities;

[TestClass]
public class OrderTests
{
    private readonly Custumer _custumer = new("Baltieri", "balta@balta.io");
    private readonly Product _product = new("Produto 1", 10, true);
    private readonly Discount _discount = new(10, DateTime.Now.AddDays(5));
    private readonly decimal _deliveryFee = 10;

    [TestMethod]
    [TestCategory("Domain")]
    public void Given_New_Valid_Order_Generates_Number_With_Eight_Characters()
    {
        var order = new Order(_custumer, _deliveryFee, _discount);
        Assert.AreEqual(8, order.Number.Length);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Given_Order_With_Waiting_Payment_Status()
    {
        var order = new Order(_custumer, _deliveryFee, _discount);
        Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Given_Order_Payment_Approved_With_Waiting_Delivery_Status()
    {
        var order = new Order(_custumer, _deliveryFee, _discount);
        order.AddItem(_product, 1);
        order.Pay(_product.Price);
        Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Given_Order_Canceled_With_Canceled_Status()
    {
        var order = new Order(_custumer, _deliveryFee, _discount);
        order.Cancel();
        Assert.AreEqual(order.Status, EOrderStatus.Canceled);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Given_New_Item_Without_Product_Should_Not_Be_Added()
    {
        var order = new Order(_custumer, _deliveryFee, _discount);
        order.AddItem(null, 0);
        Assert.AreEqual(order.Items.Count, 0);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Given_New_Item_With_Zero_Or_Less_Quantity_Should_Not_Be_Added()
    {
        var order = new Order(_custumer, _deliveryFee, _discount);
        order.AddItem(_product, -1);
        Assert.AreEqual(order.Items.Count, 0);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Given_New_Valid_Order_Total_Should_Equal_To_50()
    {
        var order = new Order(_custumer, _deliveryFee, _discount);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 50);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Given_Expired_Discount_Order_Value_Should_Be_60()
    {
        var order = new Order(_custumer, _deliveryFee, _discount);
        order.AddItem(_product, 6);
        order.Date.AddDays(6);
        Assert.AreEqual(order.Total(), 60);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Given_Invalid_Discount_Order_Value_Should_Be_60()
    {
        var order = new Order(_custumer, _deliveryFee, null);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 60);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Given_Discount_10_Order_Value_Should_Be_50()
    {
        var order = new Order(_custumer, _deliveryFee, _discount);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 50);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Given_Delivery_Fee_Equals_10_Order_Value_Should_Be_60()
    {
        var order = new Order(_custumer, _deliveryFee, _discount);
        order.AddItem(_product, 6);
        Assert.AreEqual(order.Total(), 60);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void Given_An_Order_With_No_Custumer_Order_Should_Be_Invalid()
    {
        var order = new Order(null, _deliveryFee, _discount);
        Assert.AreEqual(order.IsValid, false);
    }
}