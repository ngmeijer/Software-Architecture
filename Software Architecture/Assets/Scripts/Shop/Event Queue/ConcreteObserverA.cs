using UnityEngine;

class ConcreteObserverA : IObserver
{
    public void UpdateFromSubject(ISubject subject)
    {
        if ((subject as Subject).State < 3)
        {
            Debug.Log("ConcreteObserverA: reacted to the event.");
        }
    }
}