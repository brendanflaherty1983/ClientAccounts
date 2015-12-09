using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ClientAccounts
{
    public static class JsonHelper
    {
        public static bool WriteJsonToFile<T>(string fileName, T obj)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                    serializer.WriteObject(stream, obj);
                    string json = Encoding.UTF8.GetString(stream.ToArray());

                    File.WriteAllText(fileName, json);
                }

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public static T ReadJsonToObject<T>(string fileName) where T : new()
        {
            if (File.Exists(fileName))
            {
                using (MemoryStream ms = new MemoryStream())
                using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = File.ReadAllBytes(fileName);
                    ms.Write(bytes, 0, (int)file.Length);
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                    ms.Position = 0;
                    return (T)serializer.ReadObject(ms);
                }
            }

            return new T();
        }
    }
}
