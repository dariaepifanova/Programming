using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Proga
{
    [Serializable]
    public class Client : ISerializable
    {
        private int id;

        [NonSerialized]
        static BinaryFormatter formatter = new BinaryFormatter();

        public Client() { }
        public Client(int id, string login, string password, string name)
        {
            Name = name;
            Id = id;
            Login = login;
            Password = password;
        }

        public Client(SerializationInfo info, StreamingContext content)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            this.id = (int)info.GetValue("Id", typeof(int));
            Login = (string)info.GetValue("Login", typeof(string));
            Password = (string)info.GetValue("Password", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext content)
        {
            info.AddValue("Name", Name);
            info.AddValue("Id", this.id);
            info.AddValue("Login", Login);
            info.AddValue("Password", Password);
        }

        public string Login {
            get; 
            set; 
        }
        public string Password {
            get; 
            set; 
        }

        public string Name
        {
            get;
            set;
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Attempt to assign an invalid value to ID");
                }
                id = value;
            }
        }

        public static Client getElementById(List<Client> objects, int id) 
        {
            Client elem = null;
            foreach (Client item in objects)
            {
                if (item.Id == id)
                    elem = item;
            }
            return elem;
        }

        public static void Serialize(string fileName, List<Client> objToSerialize)
        {
            using (var fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(fStream, objToSerialize);
            }

        }

        public static List<Client> Deserialize(string fileName)
        {
            List<Client> sk = null;
            using (var fStream = File.OpenRead(fileName))
            {
                sk = (List<Client>)formatter.Deserialize(fStream);
            }
            return sk;
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}",  Id,  Login,  Password,  Name);
        }

        public static int isClient(List<Client> clients , string login, string password)
        {
            int flag = -1;
            foreach (Client item in clients)
            {
                if (item.Login == login && item.Password == password)
                {
                    flag = item.Id;
                    break;
                }
            }
            return flag;

        }
    }
}
