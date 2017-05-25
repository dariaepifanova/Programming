using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Proga
{
    [Serializable]
    public class Skates : ISerializable
    {
        private int id;
        private int size;

        [NonSerialized]
        static BinaryFormatter formatter = new BinaryFormatter();

        public Skates() { }
        public Skates(int id, bool gender, int size)
        {
            Id = id;
            Gender = gender;
            Size = size;
        }

        public override string ToString()
        {
            return String.Format("{0};{1};{2}", Id, Gender, Size);
        }

        public Skates(SerializationInfo info, StreamingContext content)
        {
            this.id = (int)info.GetValue("Id", typeof(int));
            this.size = (int)info.GetValue("Size", typeof(int));
            Gender = (bool)info.GetValue("Gender", typeof(bool));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext content)
        {
            info.AddValue("Id", this.id);
            info.AddValue("Size", this.size);
            info.AddValue("Gender", Gender);
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

        public bool Gender
        {
            get;
            set;
        }

        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                if (value < 26 || value > 47)
                    throw new ArgumentOutOfRangeException("Size is not correct!");
                size = value;
            }
        }

        public static Skates getElementById(List<Skates> objects, int id) 
        {
            Skates elem = null;
            foreach (Skates item in objects)
            {
                if (item.Id == id)
                    elem = item;
            }
            return elem;
        }

        public static void Serialize(string fileName, List<Skates> objToSerialize)
        {
            using (var fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(fStream, objToSerialize);
            }

        }

        public static List<Skates> Deserialize(string fileName)
        {
            List<Skates> sk = null;
            using (var fStream = File.OpenRead(fileName))
            {
                sk = (List<Skates>)formatter.Deserialize(fStream);
            }
            return sk;
        }

        public string toString()
        {
            return string.Format("{0},{1},{2}", Id, Gender, Size);
        }
    }
}
