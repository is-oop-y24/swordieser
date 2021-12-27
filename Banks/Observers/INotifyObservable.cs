using System.Collections.Generic;
using Banks.Messages;

namespace Banks.Observers
{
    public interface INotifyObservable
    {
        void RegisterObserver(INotifyObserver observer);

        void RemoveObserver(INotifyObserver observer);

        void SendNotify(List<INotifyObserver> observers, double amount, IBankMessage message);
    }
}