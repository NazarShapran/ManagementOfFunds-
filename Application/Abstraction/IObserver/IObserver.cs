using Domain.Transactions;

namespace Application.Abstraction.IObserver;

public interface IObserver
{
    void Update(Transaction transaction);
}