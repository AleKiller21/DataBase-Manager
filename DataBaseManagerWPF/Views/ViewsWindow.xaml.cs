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
        public ViewsWindow()
        {
            InitializeComponent();
        }

        private void btn_create_view_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_drop_view_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_generate_ddl_view_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_run_view_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = $"SELECT VIEWSCHEMA, VIEWNAME FROM SYSCAT.VIEWS WHERE VIEWSCHEMA = '{Connection.CurrentSchema}'";
            Utilities.RefreshDataGrid(dataGridViews, query);
        }
    }
}
