using System;
using System.Collections.Generic;
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

namespace DataBaseManagerWPF.Views
{
    /// <summary>
    /// Interaction logic for ViewsWindow.xaml
    /// </summary>
    public partial class ViewsWindow : Window
    {
        private readonly string _projectionQuery;

        public ViewsWindow()
        {
            InitializeComponent();
            _projectionQuery =
                $"SELECT VIEWSCHEMA, VIEWNAME FROM SYSCAT.VIEWS WHERE VIEWSCHEMA = '{Connection.CurrentSchema}'";
        }

        private void btn_create_view_Click(object sender, RoutedEventArgs e)
        {
            new SqlEditorWindow("CREATE OR REPLACE VIEW <VIEW_NAME> AS\n\t<PROJECTION>").Show();
        }

        private void btn_drop_view_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_generate_ddl_view_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.RefreshDataGrid(dataGridViews, _projectionQuery);
        }
    }
}
