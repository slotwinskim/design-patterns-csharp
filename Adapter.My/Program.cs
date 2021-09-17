using System;

namespace Adapter.My
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var xmlDataProvider = new XmlDataProvider();
            Console.WriteLine(xmlDataProvider.GetXml1());

            var xmlToJsonAdapter = new XmlToJsonAdapter(xmlDataProvider);
            Console.WriteLine(xmlToJsonAdapter.GetJson1());
        }
    }

    public interface IXmlToJsonAdapter
    {
        string GetJson1();
        string GetJson2();
    }

    public class XmlDataProvider
    {
        public string GetXml1()
        {
            return "stuff";
        }

        public string GetXml2()
        {
            return "stuff1";
        }

        public string GetXml3()
        {
            return "stuff";
        }
    }

    public class XmlToJsonAdapter : IXmlToJsonAdapter
    {
        private readonly XmlDataProvider xmlDataProvider;

        public XmlToJsonAdapter(XmlDataProvider xmlDataProvider)
        {
            this.xmlDataProvider = xmlDataProvider;
        }

        public string GetJson1()
        {
            return $"JSON({xmlDataProvider.GetXml1()})";
        }

        public string GetJson2()
        {
            return $"JSON({xmlDataProvider.GetXml2()})";
        }
    }
}
