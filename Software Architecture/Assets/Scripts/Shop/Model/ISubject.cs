public interface ISubject
{
    void Attach(IObserver  pObserver);

    void Detach(IObserver pObserver);

    void NotifyObservers();
}