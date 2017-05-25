using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.IO;
using Microsoft.Win32;

namespace Proga
{
    /// <summary>
    /// Interaction logic for StartWin.xaml
    /// </summary>
    public partial class StartWin : Window
    {
       
        List<Client> clients = null;
        List<Skates> skates = null;
        List<Order> orders = null;
        
        
        public StartWin()
        {
            InitializeComponent();
        }

        private void SkatesBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    skates = FileProcessing.CreateListSkates(new FileInfo(openFileDialog.FileName));
                    SkatesPath.Text = openFileDialog.FileName;
                }
                catch { }

                if (skates == null)
                {
                    try
                    {
                        skates = Skates.Deserialize(openFileDialog.FileName);
                        SkatesPath.Text = openFileDialog.FileName;
                    }
                    catch
                    {
                        MessageBox.Show("File is not correct. Choose another one.");
                    }
                }
            }
        }

        private void OrderssBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    orders = FileProcessing.CreateListOrder(new FileInfo(openFileDialog.FileName), clients, skates);
                    OrderPath.Text = openFileDialog.FileName;
                }
                catch
                { }
                if (orders == null)
                {
                    try
                    {
                        orders = Order.Deserialize(openFileDialog.FileName);
                        OrderPath.Text = openFileDialog.FileName;
                    }
                    catch
                    {
                        MessageBox.Show("File is not correct. Choose another one.");
                    }
                }
            }
        }

        private void ClientsBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    clients = FileProcessing.CreateListClient(new FileInfo(openFileDialog.FileName));
                    ClientPath.Text = openFileDialog.FileName;
                }
                catch
                {

                }
                if (clients == null)
                {
                    try
                    {
                        clients = Client.Deserialize(openFileDialog.FileName);
                        ClientPath.Text = openFileDialog.FileName;
                    }
                    catch
                    {
                        MessageBox.Show("File is not correct. Choose another one.");
                    }
                }
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login(clients, skates, orders);
            log.Show();
            this.Hide();
        }
    }
}
