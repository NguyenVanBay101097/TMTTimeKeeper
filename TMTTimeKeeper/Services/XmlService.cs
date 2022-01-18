using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TMTTimeKeeper.Interface;

namespace TMTTimeKeeper.Services
{
    public class XmlService : IXmlService
    {
        public T FromXML<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }

        public string ToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        public void WriteXMLFile<T>(string path, T obj, string rootEl = "Data")
        {
            XmlSerializer serialiser = new XmlSerializer(typeof(T), new XmlRootAttribute(rootEl));
            TextWriter filestream = new StreamWriter(path);
            serialiser.Serialize(filestream, obj);
            filestream.Close();
        }

        public T GetObject<T>(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(stream);
            }
        }

        public void ChangeTextInNode(string path,string nodeName, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path); 
            doc.SelectSingleNode(nodeName).InnerText = value;
        }
    }


}
