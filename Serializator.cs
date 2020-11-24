using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace The_Hangman_Game
{

    internal static class Serializator
    {
        private static IFormatter formatter = new BinaryFormatter();

        public static void Serialize(string fileName, Object objToSerialize)
        {
            using (Stream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                try
                {
                    formatter.Serialize(stream, objToSerialize);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    throw;
                }
            }
        }
        public static object Deserialize(string fileName)
        {
            object item;
            using (Stream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read))
            {
                try
                {
                    item = formatter.Deserialize(stream);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("You are the first Player - no History records found");
                    throw;
                }
            }
            return item;
        }
    }
}