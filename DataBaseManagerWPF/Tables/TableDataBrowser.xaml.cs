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

namespace DataBaseManagerWPF.Tables
{
    /// <summary>
    /// Interaction logic for TableDataBrowser.xaml
    /// </summary>
    public partial class TableDataBrowser : Window
    {
        private readonly string _scheme;
        private readonly string _table;

        public TableDataBrowser(string scheme, string table)
        {
            InitializeComponent();
            _scheme = scheme;
            _table = table;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = $"SELECT * FROM {_scheme}.{_table}";
            Utilities.RefreshDataGrid(dataGrid, query);
        }

        private void btn_insert_Click(object sender, RoutedEventArgs e)
        {
            var query = $"INSERT INTO {_scheme}.{_table} VALUES (<VALUES>)";
            new SqlEditorWindow(query).Show();
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            var row = dataGrid.SelectedItem as DataRowView;
            if (row == null) return;

            var query = $"DELETE FROM {_scheme}.{_table} WHERE {dataGrid.Columns[0].Header} = {row[0]}";
            new SqlEditorWindow(query).Show();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            var row = dataGrid.SelectedItem as DataRowView;
            if(row == null) return;

            var query = $"UPDATE {_scheme}.{_table} SET <VALUES> WHERE {dataGrid.Columns[0].Header} = {row[0]}";
            new SqlEditorWindow(query).Show();
        }
    }
}
