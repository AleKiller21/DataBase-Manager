using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
//using System.Windows.Forms;
using System.Windows.Input;
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

        private void dataGrid_Connections_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var conn = dataGrid_Connections.SelectedItem as ConnectionItem;
            if (conn == null) return;
            var name = conn.Connection;
            var connString = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[name].ConnectionString).DataSource;
            
            Connection.Connect(connString);
        }
    }
}
