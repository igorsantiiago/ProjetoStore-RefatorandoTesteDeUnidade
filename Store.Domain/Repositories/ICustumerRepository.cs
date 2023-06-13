using Store.Domain.Entities;

namespace Store.Domain.Repositories.Interfaces;

public interface ICustumerRepository
{
    Custumer Get(string document);
}