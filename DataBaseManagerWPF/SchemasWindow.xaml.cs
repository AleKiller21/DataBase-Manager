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

namespace DataBaseManagerWPF
{
    /// <summary>
    /// Interaction logic for SchemasWindow.xaml
    /// </summary>
    public partial class SchemasWindow : Window
    {
        private readonly string _projectionQuery;

        public SchemasWindow()
        {
            InitializeComponent();

            _projectionQuery = "SELECT SCHEMANAME, OWNER FROM SYSCAT.SCHEMATA";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.RefreshDataGrid(dataSchemasGrid, _projectionQuery);
        }

        private void dataSchemasGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = dataSchemasGrid.SelectedItem as DataRowView;
            if(row == null) return;

            Connection.CurrentSchema = row["SCHEMANAME"].ToString().Trim();
            Close();
        }
    }
}
