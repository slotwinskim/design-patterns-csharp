using System;

namespace Decorator.My
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var component = new ConcreteComponent();

            var decorator = new ConcreteDecorator(component);
            var decorator1 = new ConcreteDecorator(decorator);

            var client = new Client();
            var result = client.ClientCode(decorator1);
            Console.WriteLine(result);
        }

        public abstract class Component
        {
            public abstract string DoStuff();
        }

        public class ConcreteComponent : Component
        {
            public override string DoStuff()
            {
                return "From ConcreteComponent";
            }
        }

        public abstract class Decorator : Component
        {
            protected readonly Component component;

            public Decorator(Component component)
            {
                this.component = component;
            }

            public override string DoStuff()
            {
                return component is not null
                    ? component.DoStuff()
                    : string.Empty;
            }
        }

        public class ConcreteDecorator : Decorator
        {
            public ConcreteDecorator(Component component)
                : base(component)
            {
            }

            public override string DoStuff()
            {
                return $"From ConcreteDecorator '{base.DoStuff()}'";
            }
        }

        public class Client
        {
            public string ClientCode(Component component)
            {
                return component.DoStuff();
            }
        }
    }
}
