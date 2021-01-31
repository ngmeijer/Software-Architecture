public interface ISubject
{
    int SubjectState { get; set; }

    void Attach(IObserver  pObserver);

    void Detach(IObserver pObserver);

    void NotifyObservers();
}