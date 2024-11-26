using Newtonsoft.Json;
using System;
using System.IO;

namespace ModelsLib.Storage
{
    public class JsonFileStorage : ISave, ILoad
    {
        private readonly string fileName;

        public JsonFileStorage(string filename = "data.json")
        {
            fileName = filename;
        }

        public T Load<T>()
        {
            try
            {
                if (!File.Exists(fileName))
                    return default(T);

                using (var file = File.OpenText(fileName))
                {
                    var serializer = new JsonSerializer
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                        NullValueHandling = NullValueHandling.Ignore
                    };
                    return (T)serializer.Deserialize(file, typeof(T));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                return default(T);
            }
        }

        public void Save<T>(T obj)
        {
            try
            {
                using (var file = File.CreateText(fileName))
                {
                    var serializer = new JsonSerializer
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                        NullValueHandling = NullValueHandling.Ignore,
                        Formatting = Formatting.Indented
                    };
                    serializer.Serialize(file, obj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to JSON file: {ex.Message}");
            }
        }
    }
}
