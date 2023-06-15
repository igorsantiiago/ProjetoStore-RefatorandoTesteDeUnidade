using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories.Interfaces;
using Store.Domain.Utils;

namespace Store.Domain.Handlers;

public class OrderHandler : Notifiable<Notification>, IHandler<CreateOrderCommand>
{
    private readonly ICustumerRepository _custumerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderHandler(ICustumerRepository custumerRepository, IDeliveryFeeRepository deliveryFeeRepository, IDiscountRepository discountRepository, IProductRepository productRepository, IOrderRepository orderRepository)
    {
        _custumerRepository = custumerRepository;
        _deliveryFeeRepository = deliveryFeeRepository;
        _discountRepository = discountRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public ICommandResult Handle(CreateOrderCommand command)
    {
        //Fail Fast Validation
        command.Validate();
        if (!command.IsValid)
            return new GenericCommandResult(false, "Pedido Inválido", null);

        // 1. Recover Client
        var custumer = _custumerRepository.Get(command.Custumer);

        // 2. Delivery Fee
        var deliveryFee = _deliveryFeeRepository.Get(command.Zipcode);

        // 3. Discount
        var discount = _discountRepository.Get(command.PromoCode);

        // 4. Create Order
        var products = _productRepository.Get(ExtractGuids.Extract(command.Items)).ToList();
        var order = new Order(custumer, deliveryFee, discount);
        foreach (var item in command.Items)
        {
            var product = products.Where(x => x.Id == item.Product).FirstOrDefault();
            order.AddItem(product, item.Quantity);
        }

        // 5. Group Notifications
        AddNotifications(order.Notifications);

        // 6. Check if is everything is valid
        if (!IsValid)
            return new GenericCommandResult(false, "Falha ao gerar pedido.", Notifications);

        // 7. Return result
        _orderRepository.Save(order);
        return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso", order);
    }
}
