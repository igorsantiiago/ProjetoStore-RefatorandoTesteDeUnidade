using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;

namespace Store.Domain.Commands;

public class CreateOrderItemCommand : Notifiable<Notification>, ICommand
{
    public CreateOrderItemCommand()
    {

    }

    public CreateOrderItemCommand(Guid product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public Guid Product { get; set; }
    public int Quantity { get; set; }
    // public Contract<T> IsLowerThan(string val, int comparer, string key, string message);
    public void Validate()
    {
        AddNotifications(new Contract<CreateOrderItemCommand>().Requires().AreEquals(Product.ToString().Length, 32, nameof(Product), "Produto Invalido").IsGreaterThan(Quantity, 0, nameof(Quantity), "Quantidade deve ser maior que 0"));
    }
}
