public interface ISubject
{
    bool ListHasDecreasedSize { get; set; }
    bool ListHasItemUpgraded { get; set; }

    void Attach(IObserver  pObserver);

    void Detach(IObserver pObserver);

    void NotifyObservers();
}