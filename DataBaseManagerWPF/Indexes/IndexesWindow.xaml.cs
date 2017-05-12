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

namespace DataBaseManagerWPF.Indexes
{
    /// <summary>
    /// Interaction logic for IndexesWindow.xaml
    /// </summary>
    public partial class IndexesWindow : Window
    {
        private string _projectionQuery;
        public IndexesWindow()
        {
            InitializeComponent();
            _projectionQuery =
                $"SELECT INDSCHEMA, INDNAME, TABNAME, UNIQUERULE FROM SYSCAT.INDEXES WHERE INDSCHEMA = '{Connection.CurrentSchema}'";
        }

        private void btn_create_index_Click(object sender, RoutedEventArgs e)
        {
            var indexTypeWindow = new IndexTypeWindow();
            indexTypeWindow.ShowDialog();
        }

        private void btn_drop_index_Click(object sender, RoutedEventArgs e)
        {
            var row = dataGridIndexes.SelectedItem as DataRowView;
            if (row == null) return;

            if (row["UNIQUERULE"].ToString().Equals("P"))
                MessageBox.Show(
                    $"Alter cannot be performed for the following reasons:\r\n\r\n{row["INDNAME"]} is a system-generated index.");

            else new SqlEditorWindow(Index.GenerateDropDDL(row["INDSCHEMA"].ToString(), row["INDNAME"].ToString())).Show();
        }

        private void btn_alter_index_Click(object sender, RoutedEventArgs e)
        {
            var row = dataGridIndexes.SelectedItem as DataRowView;
            if (row == null) return;

            if (row["UNIQUERULE"].ToString().Equals("P"))
                MessageBox.Show(
                    $"Alter cannot be performed for the following reasons:\r\n\r\n{row["INDNAME"]} is a system-generated index.");

            else
            {
                var ddl = Index.GenerateDropDDL(row["INDSCHEMA"].ToString(), row["INDNAME"].ToString());
                ddl += "\n" + Index.GenerateDDL(row["INDSCHEMA"].ToString(), row["INDNAME"].ToString());

                new SqlEditorWindow(ddl).Show();
            }
        }

        private void btn_generate_ddl_index_Click(object sender, RoutedEventArgs e)
        {
            var row = dataGridIndexes.SelectedItem as DataRowView;
            if(row == null) return;

            var schema = row["INDSCHEMA"];
            var name = row["INDNAME"];

            var sqlEditor = new SqlEditorWindow(Index.GenerateDDL(schema.ToString(), name.ToString()));
            sqlEditor.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.RefreshDataGrid(dataGridIndexes, _projectionQuery);
        }
    }
}
