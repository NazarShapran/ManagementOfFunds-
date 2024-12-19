using ManagementOfFunds.Implementation;

namespace ManagementOfFunds.Abstraction;

public interface IObserver
{
    void Update(Transaction transaction);
}