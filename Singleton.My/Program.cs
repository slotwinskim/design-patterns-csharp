using System;

namespace Singleton.My
{
    class Program
    {
        static void Main()
        {
            var name = SingletonBase<Name>.Instance;
            Console.WriteLine($"{name.First} -- {name.Last} -- {name.Birthday}");

            var name1 = SingletonBase<Name>.Instance;
            Console.WriteLine($"{name.First} -- {name.Last} -- {name.Birthday}");
        }

        public class Name
        {
            public string First => "first";
            public string Last => "last";
            public DateTime Birthday => DateTime.Now;
        }

        public sealed class SingletonBase<T>
        {
            private SingletonBase() { }

            private static readonly Lazy<T> lazy = new();

            public static T Instance => lazy.Value;
        }
    }
}