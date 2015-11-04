using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Utils
{
    public class JsonHelper
    {
        /// <summary>
        /// Serialize Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray(), 0, (int)ms.Length);
            ms.Dispose();
            return retVal;
        }

        /// <summary>
        /// Deserialze Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json) where T : new()
        {
            using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(memoryStream);
            }
        }

        /// <summary>
        /// Deserialze Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static T Deserialize<T>(MemoryStream stream) where T : new()
        {
            using (MemoryStream memoryStream = stream)
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(memoryStream);
            }
        }

    }
}
