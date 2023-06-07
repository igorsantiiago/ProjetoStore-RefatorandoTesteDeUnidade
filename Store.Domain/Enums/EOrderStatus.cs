namespace Store.Domain.Entities;

public enum EOrderStatus
{
    WaitingPayment = 1,
    WaitingDelivery = 2,
    Canceled = 3
}