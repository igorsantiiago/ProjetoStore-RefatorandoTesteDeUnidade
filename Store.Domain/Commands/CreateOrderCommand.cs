using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands;

public class CreateOrderCommand : Notifiable<Notification>, ICommand
{
    public CreateOrderCommand()
    {
        Items = new List<CreateOrderItemCommand>();
    }

    public CreateOrderCommand(string custumer, string zipcode, string promoCode, IList<CreateOrderItemCommand> items)
    {
        Custumer = custumer;
        Zipcode = zipcode;
        PromoCode = promoCode;
        Items = items;
    }

    public string Custumer { get; set; } = string.Empty;
    public string Zipcode { get; set; } = string.Empty;
    public string PromoCode { get; set; } = string.Empty;
    public IList<CreateOrderItemCommand> Items { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<CreateOrderCommand>().Requires().AreEquals(Custumer, 11, "Custumer", "Cliente Inválido.").AreEquals(Zipcode, 8, "ZipCode", "CEP Inválido."));
    }
}
