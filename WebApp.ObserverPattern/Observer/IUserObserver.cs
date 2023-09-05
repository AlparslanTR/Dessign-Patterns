using Base.Main.Models;

namespace WebApp.ObserverPattern.Observer
{
    public interface IUserObserver
    {
        void CreateUser(Customer customer);
    }
}
