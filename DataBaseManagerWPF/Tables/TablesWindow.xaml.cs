using System.Data;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using DataBaseLayer;
using DataBaseLayer.Models;
using IBM.Data.DB2;

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
                $"SELECT TABSCHEMA, TABNAME FROM SYSCAT.TABLES WHERE TABSCHEMA = '{Connection.CurrentSchema}'";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.RefreshDataGrid(dataGridTables, _projectionQuery);
        }

        private void btn_create_table_Click(object sender, RoutedEventArgs e)
        {
            const string createCommand = "CREATE TABLE <NAME> (<FIELDS>)";
            var editor = new SqlEditorWindow(createCommand);
            editor.ShowDialog();
        }

        private void btn_generate_ddl_table_Click(object sender, RoutedEventArgs e)
        {
            var dataRowView = dataGridTables.SelectedItem as DataRowView;
            if (dataRowView == null) return;

            var tabSchema = dataRowView.Row.ItemArray[0];
            var tabName = dataRowView.Row.ItemArray[1];

            var editor = new SqlEditorWindow(Table.GenerateDDL(tabName.ToString(), tabSchema.ToString()));
            editor.Show();
        }

        private void btn_drop_table_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_alter_table_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
