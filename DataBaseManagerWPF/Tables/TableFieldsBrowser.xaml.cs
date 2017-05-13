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
    /// Interaction logic for TableFieldsBrowser.xaml
    /// </summary>
    public partial class TableFieldsBrowser : Window
    {
        private readonly string _schema;
        private readonly string _name;
        private readonly string _projectionQuery;

        public TableFieldsBrowser(string schema, string name)
        {
            InitializeComponent();
            _schema = schema;
            _name = name;
            _projectionQuery = 
                $"SELECT COLNAME, TYPENAME, LENGTH FROM SYSCAT.COLUMNS WHERE TABNAME = '{_name}' AND TABSCHEMA = '{_schema}'";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.RefreshDataGrid(dataGridFields, _projectionQuery);
        }

        private void btn_add_field_Click(object sender, RoutedEventArgs e)
        {
            var query = $"ALTER TABLE {_schema.ToUpper().Trim()}.{_name}\nADD COLUMN <COLUMN_NAME DATA_TYPE>";
            new SqlEditorWindow(query).Show();
        }

        private void btn_drop_field_Click(object sender, RoutedEventArgs e)
        {
            var row = dataGridFields.SelectedItem as DataRowView;
            if(row == null) return;

            var query = $"ALTER TABLE {_schema.ToUpper().Trim()}.{_name}\nDROP COLUMN {row["COLNAME"]}";
            new SqlEditorWindow(query).Show();
        }

        private void btn_alter_field_type_Click(object sender, RoutedEventArgs e)
        {
            var row = dataGridFields.SelectedItem as DataRowView;
            if (row == null) return;

            var query = $"ALTER TABLE {_schema.ToUpper().Trim()}.{_name}\nALTER COLUMN {row["COLNAME"]} SET DATA TYPE <TYPE>";
            new SqlEditorWindow(query).Show();
        }
    }
}
