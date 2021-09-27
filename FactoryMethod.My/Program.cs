using System;

namespace FactoryMethod.My
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new Client(new ConcreteCreator1());
            var result = client.Creator.Operate();
        }

        public class Client
        {
            public Client(Creator creator)
            {
                Creator = creator;
            }

            public Creator Creator { get; }
        }

        public interface IProduct
        {
            public string DoStuff();
        }

        public abstract class Creator
        {
            public abstract IProduct FactoryMethod();

            public string Operate()
            {
                var product = FactoryMethod();
                var result = product.DoStuff();

                return result;
            }
        }

        public class ConcreteCreator1 : Creator
        {
            public override IProduct FactoryMethod()
            {
                return new ConcreteProduct1();
            }
        }

        public class ConcreteProduct1 : IProduct
        {
            public string DoStuff()
            {
                return "Product1 has done stuff";
            }
        }
    }
}
