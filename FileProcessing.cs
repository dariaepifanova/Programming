using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;


namespace Proga
{
    public static class FileProcessing
    {
        public static string[] splitLine(string line, int len)
        {
            string[] strArr = line.Split(',');
            if (strArr.Length != len)
            {
                throw new InvalidDataException(string.Format("Line '{0}' is not correct!", line));
            }
            return strArr;
        }

        public static List<Client> CreateListClient(FileInfo file)
        {
            List<Client> clients = new List<Client>();
            using (StreamReader reader = file.OpenText())
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        break;

                    string[] strArr = splitLine(line, 4);
                    int id;
                    if (!int.TryParse(strArr[0], out id))
                    {
                        throw new InvalidDataException(string.Format("Line '{0}' is not correct!", line));
                    }
                    
                    clients.Add(new Client(id, strArr[1], strArr[2], strArr[3]));
                }
            }
            return clients;
        }

        public static List<Skates> CreateListSkates(FileInfo file)
        {
            List<Skates> skates = new List<Skates>();
            using (StreamReader reader = file.OpenText())
            {
                reader.ReadLine();
                while (true)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        break;

                    string[] strArr = splitLine(line, 3);
                    int id;
                    if (!int.TryParse(strArr[0], out id))
                    {
                        throw new InvalidDataException(string.Format("Line '{0}' is not correct!", line));
                    }
                    bool gender;
                    if (!bool.TryParse(strArr[1], out gender))
                    {
                        
                        throw new InvalidDataException(string.Format("Line '{0}' is not correct!", line));
                    }
                    int size;
                    if (!int.TryParse(strArr[2], out size))
                    {
                        throw new InvalidDataException(string.Format("Line '{0}' is not correct!", line));
                    }
                    skates.Add(new Skates(id, gender, size));
                }
                return skates;
            }
        }

        public static List<Order> CreateListOrder(FileInfo file, List<Client> clients, List<Skates> skatess)
        {
            List<Order> orders = new List<Order>();
            using (StreamReader reader = file.OpenText())
            {
                reader.ReadLine();
                while (true)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        break;

                    string[] strArr = splitLine(line, 5);
                    int id;
                    if (!int.TryParse(strArr[0], out id))
                    {
                        throw new InvalidDataException(string.Format("Line '{0}' is not correct!", line));
                    }
                    int clientId;
                    if (!int.TryParse(strArr[1], out clientId))
                    {
                        throw new InvalidDataException(string.Format("Line '{0}' is not correct!", line));
                    }
                    int skatesId;
                    if (!int.TryParse(strArr[2], out skatesId))
                    {
                        throw new InvalidDataException(string.Format("Line '{0}' is not correct!", line));
                    }
                    
                    DateTime date = DateTime.ParseExact(strArr[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    
                    int interv;
                    if (!int.TryParse(strArr[4], out interv))
                    {
                        throw new InvalidDataException(string.Format("Line '{0}' is not correct!", line));
                    }

                    orders.Add(new Order(id, Client.getElementById(clients, clientId), Skates.getElementById(skatess, skatesId), date, (intervals)interv));
                }
                return orders;
            }
        }

        

        /*public static void PrintFile(List<MosGaz> dataArr, DataGridView dataGridView1, NumericUpDown numericUpDown1)
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            dataGridView1.RowCount = (int)numericUpDown1.Value;
            foreach (MosGaz item in dataArr)
            {
                dataGridView1[0, i].Value = item.getDisNum;
                dataGridView1[1, i].Value = item.getDistrict;
                dataGridView1[2, i].Value = item.getArea;
                dataGridView1[3, i].Value = item.getStreet;
                i++;
                if (i >= numericUpDown1.Value)
                    break;
            }
        }

        public static List<Client> FilterFile(List<Client> list, string str)
        {

            return null;
        }
        public static void SortFile(byte x, List<MosGaz> processList)
        {
            switch (x)
            {
                case 1:
                    processList.Sort(CompareID);
                    break;
                case 2:
                    processList.Sort(CompareDistrict);
                    break;
                case 3:
                    processList.Sort(CompareArea);
                    break;
                case 4:
                    processList.Sort(CompareStreet);
                    break;
                default:
                    break;
            }
        }

        private static int CompareID(MosGaz x, MosGaz y)
        {
            if (x.getDisNum > y.getDisNum)
                return 1;
            if (x.getDisNum == y.getDisNum)
                return 0;
            return -1;
        }

        private static int CompareDistrict(MosGaz x, MosGaz y)
        {
            return String.Compare(x.getDistrict, y.getDistrict);
        }

        private static int CompareArea(MosGaz x, MosGaz y)
        {
            return String.Compare(x.getArea, y.getArea);
        }

        private static int CompareStreet(MosGaz x, MosGaz y)
        {
            return String.Compare(x.getStreet, y.getStreet);
        }*/
    }
}