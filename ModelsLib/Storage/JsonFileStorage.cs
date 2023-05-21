using ModelsLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace ModelsLib.Storage
{
    public sealed class JsonFileStorage : ILoad, ISave
    {
        string fileName = nameof(JsonFileStorage) + ".Json";
        public T Load<T>()
        {
            try
            {
                using (StreamReader file = File.OpenText(fileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return (T)serializer.Deserialize(file, typeof(T));
                }
            }
            catch
            { 
                return default(T);
            }
        }

        public void Save<T>(T obj)
        {
            using (StreamWriter file = File.CreateText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, obj);
            }
        }
    }
}
