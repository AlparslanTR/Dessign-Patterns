using Base.Main.Models;

namespace WebApp.ObserverPattern.Observer
{
    public class UserSubject
    {
        private readonly List<IUserObserver> _observers;

        public UserSubject()
        {
            _observers = new List<IUserObserver>();
        }


        public void RegisterObserver(IUserObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IUserObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyOberserver(Customer customer)
        {
            _observers.ForEach(x =>
            {
                x.CreateUser(customer);
            });
        }
    }
}
