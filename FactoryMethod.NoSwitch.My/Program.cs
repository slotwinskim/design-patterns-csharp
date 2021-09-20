using System;
using System.Linq;
using System.Reflection;

namespace FactoryMethod.NoSwitch.My
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var converter = ConverterFactory.Create(Converters.JsonConverter);
            Console.WriteLine(converter.Convert("test"));
        }

        public interface IConvert
        {
            public string Convert(string value);
        }

        public class JsonConverter : IConvert
        {
            public string Convert(string value)
            {
                return $"JSON'{value}'";
            }
        }

        public class XmlConverter : IConvert
        {
            public string Convert(string value)
            {
                return $"XML'{value}";
            }
        }
        public class NullConverter : IConvert
        {
            public string Convert(string value)
            {
                return $"XML'{value}";
            }
        }

        public enum Converters
        {
            JsonConverter,
            XmlConverter,
            NullConverter
        }

        public class ConverterFactory
        {
            public static IConvert Create(Converters converter)
            {
                var converterType = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(a =>
                        a.Name.Equals(converter.ToString()))
                    .DefaultIfEmpty(typeof(NullConverter))
                    .FirstOrDefault();

                return (IConvert)Activator.CreateInstance(converterType);
            }
        }
    }
}
