using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TMTTimeKeeper.Interface
{
    public interface IXmlService
    {
        string ToXML<T>(T obj);
        T FromXML<T>(string xml);
        void WriteXMLFile<T>(string path, T obj, string rootEl = "Data");
        T GetObject<T>(string path);
        void ChangeTextInNode(string path,string nodeName, string value);
    }
}
