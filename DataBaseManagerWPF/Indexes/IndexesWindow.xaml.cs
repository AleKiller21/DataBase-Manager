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

namespace DataBaseManagerWPF.Indexes
{
    /// <summary>
    /// Interaction logic for IndexesWindow.xaml
    /// </summary>
    public partial class IndexesWindow : Window
    {
        public IndexesWindow()
        {
            InitializeComponent();
        }

        private void btn_create_index_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_drop_index_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_alter_index_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_generate_ddl_index_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void RefreshIndexesDataGrid()
        {
            const string query = "SELECT INDSCHEMA, INDNAME, TABNAME, UNIQUERULE FROM SYSCAT.INDEXES WHERE INDSCHEMA = pSCHEMA";
        }
    }
}
