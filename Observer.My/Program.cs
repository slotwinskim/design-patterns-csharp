using System;
using System.Collections.Generic;

namespace Observer.My
{
    class Program
    {
        static void Main(string[] args)
        {
            var subject = new Subject();

            IObserver observer1 = new Observer();
            IObserver observer2 = new Observer();

            subject.Attach(observer1);
            subject.Attach(observer2);

            subject.Notify();
        }

        public interface IObserver
        {
            void Update();
        }

        public interface ISubject
        {
            void Attach(IObserver observer);
            void Detach(IObserver observer);
            void Notify();
        }

        public class Subject : ISubject
        {
            public int State { get; private set; } = 0;

            private List<IObserver> _observers = new();

            public void Attach(IObserver observer)
            {
                _observers.Add(observer);
            }

            public void Detach(IObserver observer)
            {
                _observers.Remove(observer);
            }

            public void Notify()
            {
                foreach (var observer in _observers)
                {
                    observer.Update();
                }
            }

            public void DoSomething()
            {
                State++;
                Notify();
            }
        }

        public class Observer : IObserver
        {
            public void Update()
            {
                Console.WriteLine($"State changed!");
            }
        }
    }
}