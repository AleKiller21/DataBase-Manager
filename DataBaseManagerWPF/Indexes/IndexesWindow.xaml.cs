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
            var indexTypeWindow = new IndexTypeWindow();
            indexTypeWindow.ShowDialog();
        }

        private void btn_drop_index_Click(object sender, RoutedEventArgs e)
        {
            //TODO Do not allow the drop or alter of primary keys
        }

        private void btn_alter_index_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_generate_ddl_index_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshIndexesDataGrid();
        }

        private void RefreshIndexesDataGrid()
        {
            var query = 
                $"SELECT INDSCHEMA, INDNAME, TABNAME, UNIQUERULE FROM SYSCAT.INDEXES WHERE INDSCHEMA = '{Connection.CurrentSchema}'";

            dataGridIndexes.ItemsSource = DBUtilities.ProjectData(query).DefaultView;
        }
    }
}
