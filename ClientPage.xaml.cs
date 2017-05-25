using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Proga
{

    class MyTable
    {
        public MyTable(int i, string g, int s)
        {
            Id = i;
            Gender = g;
            Size = s;
        }

        public int Id { get; set; }
        public string Gender { get; set; }
        public int Size { get; set; }

    }

    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Window
    {

        List<Skates> skates;
        List<Order> orders;
        List<Client> clients;
        Client client;
        Login log;


        public ClientPage(Client client, List<Skates> skates, List<Order> orders, List<Client> clients, Login log)
        {
            InitializeComponent();
   
            this.skates = skates;
            this.orders = orders;
            this.clients = clients;
            this.client = client;
            this.log = log;

            List<MyTable> table = new List<MyTable>();
            foreach (Skates i in skates)
            {
                table.Add(new MyTable(i.Id, i.Gender? "Man": "Woman", i.Size));
            }

            dataGrid.ItemsSource = table;
            
            List<intervals> inte = new List<intervals>();
            foreach (var item in Enum.GetValues(typeof(intervals)))
            {
                intervalsBox.Items.Add(item);
            }

            ClientName.Content = client.Name;
        }

        /*public List<intervals> getIntervals(List<Order> orders, Skates skate)
        {
 
        }*/

        private void SizeDown_Click(object sender, RoutedEventArgs e)
        {
            int size;
            if (!int.TryParse(SizeBox.Text, out size))
            {
                MessageBox.Show("Некорректное значение в поле 'размер'");
            }
            else
            {
                if (size >= 27 && size <= 47)
                    size--;
                else
                {
                    MessageBox.Show("Некорректное значение в поле 'размер'. Выход за пределы диапазона значений (26, 47)");
                }
                SizeBox.Text = "" + size;
            }
        }

        private void SizeUp_Click(object sender, RoutedEventArgs e)
        {
            int size;
            if (!int.TryParse(SizeBox.Text, out size))
            {
                MessageBox.Show("Некорректное значение в поле 'размер'");
            }
            else
            {
                if (size >= 26 && size <= 46)
                    size++;
                else
                {
                    MessageBox.Show("Некорректное значение в поле 'размер'. Выход за пределы диапазона значений (26, 47)");
                }
                SizeBox.Text = "" + size;
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                //orders.Add(new Order(-1,client, Skates.getElementById(dataGrid.Rows[dataGrid.SelectedIndex], 0]),)
            }
        }

        private void Exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            log.Show();
            Order.Serialize("serialised_orders.txt", orders);
            Client.Serialize("serialized_client.txt", clients);
            Skates.Serialize("serialized_skates.txt", skates);
            this.Close();
        }
    }
}