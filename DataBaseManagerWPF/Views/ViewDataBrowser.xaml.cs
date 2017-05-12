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

namespace DataBaseManagerWPF.Views
{
    /// <summary>
    /// Interaction logic for ViewDataBrowser.xaml
    /// </summary>
    public partial class ViewDataBrowser : Window
    {
        private readonly string _viewSchema;
        private readonly string _viewName;

        public ViewDataBrowser(string schema, string name)
        {
            InitializeComponent();

            _viewSchema = schema;
            _viewName = name;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var reader = View.GetData(_viewSchema, _viewName);
                var data = new DataTable();
                data.Load(reader);
                dataGrid.ItemsSource = data.DefaultView;
                reader.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Close();
            }
        }
    }
}
