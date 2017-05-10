using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataBaseLayer;
using IBM.Data.DB2;

namespace DataBaseManagerWPF.Tables
{
    /// <summary>
    /// Interaction logic for TablesWindow.xaml
    /// </summary>
    public partial class TablesWindow : Window
    {
        public TablesWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshTableDataGrid();
        }

        private void btn_create_table_Click(object sender, RoutedEventArgs e)
        {
            const string createCommand = "CREATE TABLE <NAME> (<FIELDS>)";
            var editor = new SqlEditorWindow(createCommand);
            editor.ShowDialog();
            RefreshTableDataGrid();
        }

        private void RefreshTableDataGrid()
        {
            const string query = "SELECT TABSCHEMA, TABNAME, OWNER FROM SYSCAT.TABLES WHERE TABSCHEMA = 'BLUADMIN'";
            var db2Command = new DB2Command(query, Connection.CurrentConnection);
            var result = db2Command.ExecuteReader();
            var data = new DataTable();
            data.Load(result);
            dataGridTables.ItemsSource = data.DefaultView;
        }
    }
}
