// https://www.martinfowler.com/eaaDev/EventAggregator.html
// Channel events from multiple objects into a single object to simplify registration for clients.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Observer.EventAggregator.My
{
    internal class Program
    {
        static void Main()
        {
            var eventAggregator = new EventAggregator();
            var doSomethingAfterPatientAdded = new DoSomethingWithAddingPatient();

            eventAggregator.SubscribeTo(new PatientAdded(0), doSomethingAfterPatientAdded);

            var addsPatient = new AddsPatient(eventAggregator);
            addsPatient.DoStuff();
        }

        public class DoSomethingWithAddingPatient : ISubscriber
        {
            public void Notify(IEvent @event)
            {
                if (@event is PatientAdded patientAddEvent)
                    Console.WriteLine($"Patient added with Id {patientAddEvent.Id}");
            }
        }

        public class AddsPatient
        {
            private readonly EventAggregator eventAggregator;

            public AddsPatient(EventAggregator eventAggregator)
            {
                this.eventAggregator = eventAggregator;
            }

            public void DoStuff()
            {
                eventAggregator.Publish(new PatientAdded(3));
            }
        }

        public interface IEvent { }

        public interface ISubscriber
        {
            public void Notify(IEvent @event);
        }

        public class PatientAdded : IEvent
        {
            public PatientAdded(int id)
            {
                Id = id;
            }

            public int Id { get; }
        }

        public class EventAggregator
        {
            private readonly ConcurrentDictionary<Type, List<ISubscriber>> events = new();
            public void SubscribeTo(IEvent @event, ISubscriber subscruber)
            {
                if (events.TryGetValue(@event.GetType(), out var existing))
                {
                    existing.Add(subscruber);
                    return;
                }

                events.TryAdd(@event.GetType(), new List<ISubscriber> { subscruber });
            }

            public void Publish(IEvent @event)
            {
                if(events.TryGetValue(@event.GetType(), out var publishers))
                {
                    foreach (var publisher in publishers)
                    {
                        publisher.Notify(@event);
                    }
                }
            }
        }
    }
}
