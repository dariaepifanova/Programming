using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Proga
{
    public enum intervals { _10to12 = 1, _12to14 = 2, _14to16 = 3, _16to18 = 4, _18to20 = 5, _20to22 = 6 };
    
    [Serializable]
    public class Order : ISerializable
    {
        
        private int id;
        private Client client;
        private Skates skates;
        
        [NonSerialized]
        static BinaryFormatter formatter = new BinaryFormatter();

        public Order() { }

        public Order(int id, Client client, Skates skates, DateTime date, intervals interval)
        {
            Id = id;
            Client = client;
            Skates = skates;
            Interval = interval;
            Date = date;
        }

        public Order(SerializationInfo info, StreamingContext content)
        {
            Id = (int)info.GetValue("Id", typeof(int));
            Client = (Client)info.GetValue("Client", typeof(Client));
            Skates = (Skates)info.GetValue("Skates", typeof(Skates));
            Interval = (intervals)info.GetValue("Interval", typeof(intervals));
            Date = (DateTime)info.GetValue("Date", typeof(DateTime));

        }

        public void GetObjectData(SerializationInfo info, StreamingContext content)
        {
            info.AddValue("Id", Id);
            info.AddValue("Client", Client);
            info.AddValue("Skates", Skates);
            info.AddValue("Interval", Interval);
            info.AddValue("Date", Date);
        }

        public intervals Interval
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

        public Skates Skates
        {
            get
            {
                return skates;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Skates is null!");
                skates = value;
            }
        }

        public Client Client
        {
            get
            {
                return client;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Client is null!");
                client = value;
            }
        }

        public DateTime Date
        {
            get;
            set;
        }

        public static void Serialize(string fileName, List<Order> objToSerialize)
        {
            using (var fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(fStream, objToSerialize);
            }

        }

        public static List<Order> Deserialize(string fileName)
        {
            List<Order> o = null;
            using (var fStream = File.OpenRead(fileName))
            {
                o = (List<Order>)formatter.Deserialize(fStream);
            }
            return o;
        }

        public override string ToString()
        {
            return String.Format("{0},{1},{2},{3},{4}",Id, Client.Id, Skates.Id, Date.ToString("yyyy-MM-dd"), (int)Interval);
        }
    }
}