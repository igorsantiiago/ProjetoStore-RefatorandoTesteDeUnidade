using Store.Domain.Entities;
using Store.Domain.Repositories.Interfaces;

namespace Store.Tests.Repositories.Mocks;

public class FakeCustumerRepository : ICustumerRepository
{
    public Custumer Get(string document)
    {
        if (document == "01234567891")
            return new Custumer("Baltieri", "balta@balta.io");

        return null;
    }
}