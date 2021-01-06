using UnityEngine;

class ConcreteObserverB : IObserver
{
    public void UpdateFromSubject(ISubject subject)
    {
        if ((subject as BuyModel).State == 0 || (subject as BuyModel).State >= 2)
        {
            Debug.Log("ConcreteObserverB: reacted to the event.");
        }
    }
}