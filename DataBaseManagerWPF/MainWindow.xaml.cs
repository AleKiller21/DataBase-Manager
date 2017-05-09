using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataBaseLayer;
using DataBaseLayer.Models;

namespace DataBaseManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<ConnectionItem> _connections;

        public MainWindow()
        {
            InitializeComponent();
            _connections = new ObservableCollection<ConnectionItem>();
            DataContext = _connections;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            CreateConnection newConn = new CreateConnection();
            newConn.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var savedConnections = Connection.GetConnections();

            foreach (var conn in savedConnections)
            {
                conn.Source = conn.Source.Split(':')[0].Substring(7);
                _connections.Add(conn);
            }
        }
    }
}
