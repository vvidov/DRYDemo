using ModelsLib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModelsLib.Storage
{
    public class XmlFileStorage : IModelStorage
    {
        string fileName = nameof(XmlFileStorage) + ".xml";
        public T Load<T>()
        {
            try
            {
                XmlSerializer ser = XmlSerializer.FromTypes(new Type[] { typeof(T) })[0];
                using (StreamReader sr = new StreamReader(fileName))
                {
                    return (T)ser.Deserialize(sr);
                }
            }
            catch(Exception ex )
            {
                return default(T);
            }
        }

        public void Save<T>(T obj)
        {
            XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new Type[] { typeof(T) })[0];
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                xmlSerializer.Serialize(writer, obj);
            }
        }
    }
}
