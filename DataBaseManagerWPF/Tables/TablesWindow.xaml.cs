using System;
using System.Data;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using DataBaseLayer;
using DataBaseLayer.Models;
using IBM.Data.DB2;
using MessageBox = System.Windows.MessageBox;

namespace DataBaseManagerWPF.Tables
{
    /// <summary>
    /// Interaction logic for TablesWindow.xaml
    /// </summary>
    public partial class TablesWindow : Window
    {
        private readonly string _projectionQuery;

        public TablesWindow()
        {
            InitializeComponent();
            _projectionQuery =
                $"SELECT TABSCHEMA, TABNAME FROM SYSCAT.TABLES WHERE TABSCHEMA = '{Connection.CurrentSchema}' AND TYPE = 'T'";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.RefreshDataGrid(dataGridTables, _projectionQuery);
        }

        private void btn_create_table_Click(object sender, RoutedEventArgs e)
        {
            const string createCommand = "CREATE TABLE <NAME> (<FIELDS>) ORGANIZE BY ROW";
            var editor = new SqlEditorWindow(createCommand);
            editor.ShowDialog();
        }

        private void btn_generate_ddl_table_Click(object sender, RoutedEventArgs e)
        {
            var dataRowView = dataGridTables.SelectedItem as DataRowView;
            if (dataRowView == null) return;

            var tabSchema = dataRowView.Row.ItemArray[0];
            var tabName = dataRowView.Row.ItemArray[1];

            try
            {
                var editor = new SqlEditorWindow(Table.GenerateDDL(tabName.ToString(), tabSchema.ToString()));
                editor.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btn_drop_table_Click(object sender, RoutedEventArgs e)
        {
            var row = dataGridTables.SelectedItem as DataRowView;
            if (row == null) return;

            var query = $"DROP TABLE {row["TABSCHEMA"].ToString()}.{row["TABNAME"].ToString()}";
            new SqlEditorWindow(query).Show();
        }

        private void btn_alter_table_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dataGridTables_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = dataGridTables.SelectedItem as DataRowView;
            if(row == null) return;

            new TableDataBrowser(row["TABSCHEMA"].ToString(), row["TABNAME"].ToString()).Show();
        }
    }
}
