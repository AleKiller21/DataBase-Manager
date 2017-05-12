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

namespace DataBaseManagerWPF.Triggers
{
    /// <summary>
    /// Interaction logic for TriggersWindow.xaml
    /// </summary>
    public partial class TriggersWindow : Window
    {
        private readonly string _projectionQuery;
        public TriggersWindow()
        {
            InitializeComponent();
            _projectionQuery = $"SELECT TRIGSCHEMA, TRIGNAME, TABNAME, TRIGTIME, TRIGEVENT FROM SYSCAT.TRIGGERS WHERE TRIGSCHEMA = '{Connection.CurrentSchema}'";
        }

        private void btn_create_trigger_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_drop_trigger_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_trigger_ddl_trigger_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.RefreshDataGrid(dataGridTriggers, _projectionQuery);
        }
    }
}
