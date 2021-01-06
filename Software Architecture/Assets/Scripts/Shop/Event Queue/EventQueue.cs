using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Object = System.Object;

public class EventQueue : MonoBehaviour
{
    private void Start()
    {
        Subject subject = new Subject();
        ConcreteObserverA observerA = new ConcreteObserverA();

        subject.Attach(observerA);

        ConcreteObserverB observerB = new ConcreteObserverB();
        subject.Attach(observerB);

        subject.TestFunction();
        subject.TestFunction();

        subject.Detach(observerB);

        subject.TestFunction();
    }
}