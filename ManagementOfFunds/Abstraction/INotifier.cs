using ManagementOfFunds.Implementation;

namespace ManagementOfFunds.Abstraction;

public interface INotifier
{
    void Subscribe(IObserver observer);
    void Unsubscribe(IObserver observer);
    void Notify(Transaction transaction);
}