using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Proga
{
    public class Serializer
    {
        public Serializer() { }

        public void SerializeObject(string fileName, SerializableObject objToSerialize)
        {
            FileStream fstream = File.Open(fileName, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fstream, objToSerialize);
            fstream.Close();
        }

        public SerializableObject DeserializeObject(string fileName)
        {
            SerializableObject objToSerialize = null;
            FileStream fstream = File.Open(fileName, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            objToSerialize = (SerializableObject)binaryFormatter.Deserialize(fstream);
            fstream.Close();
            return objToSerialize;
        }

    }

    [Serializable]
    public class SerializableObject : ISerializable
    {
        private List<Client> clients;

        public List<Client> Clients
        {
            get { return this.clients; }
            set { this.clients = value; }
        }
        public SerializableObject() { }

        public SerializableObject(SerializationInfo sInfo, StreamingContext contextArg)
        {
            this.clients = (List<Client>)sInfo.GetValue("Clients", typeof(List<Client>));
        }

        public void GetObjectData(SerializationInfo sInfo, StreamingContext contextArg)
        {
            sInfo.AddValue("Clients", this.clients);
        }
    }
}
