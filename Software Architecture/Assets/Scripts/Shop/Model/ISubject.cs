public interface ISubject
{
    //Coupled to ShopActions enum
    int SubjectState { get; set; }

    Item tradedItem { get; set; }

    void Attach(IObserver  pObserver);

    void Detach(IObserver pObserver);

    void NotifyObservers();
}