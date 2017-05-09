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
        //TODO Permitir eliminar una conexion

        private ObservableCollection<ConnectionItem> _connections;

        public MainWindow()
        {
            InitializeComponent();
            _connections = new ObservableCollection<ConnectionItem>();
            DataContext = _connections;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            CreateConnection newConn = new CreateConnection("", "", "", "", "", "");
            newConn.ShowDialog();
            RefreshConnectionDataGrid();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshConnectionDataGrid();
        }

        private void RefreshConnectionDataGrid()
        {
            var savedConnections = Connection.GetConnections();

            _connections.Clear();
            foreach (var conn in savedConnections)
            {
                conn.Source = conn.Source.Split(':')[0].Substring(7);
                _connections.Add(conn);
            }
        }

        private void dataGrid_Connections_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var name = GetConnectionSelectedName();
            //var connString = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[name].ConnectionString).DataSource;

            //Connection.Connect(connString);
            
            var manager = new ManagerWindow();
            manager.Show();
            Close();
        }

        private void btn_edit_connection_Click(object sender, RoutedEventArgs e)
        {
            if(dataGrid_Connections.SelectedIndex < 0) return;

            var name = GetConnectionSelectedName();
            var connSettings = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[name].ConnectionString);
            var server = connSettings.DataSource.Split(';')[0];
            var host = server.Split(':')[0].Substring(7);
            var port = server.Split(':')[1];
            
            var connWindow = new CreateConnection(connSettings.InitialCatalog, host, port, connSettings.UserID, connSettings.Password, name);
            connWindow.ShowDialog();
            RefreshConnectionDataGrid();
        }

        private string GetConnectionSelectedName()
        {
            var conn = dataGrid_Connections.SelectedItem as ConnectionItem;
            return conn?.Connection;
        }

    }
}
