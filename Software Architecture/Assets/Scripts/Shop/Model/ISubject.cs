public interface ISubject
{
    bool ListHasChanged { get; set; }

    void Attach(IObserver  pObserver);

    void Detach(IObserver pObserver);

    void NotifyObservers();
}