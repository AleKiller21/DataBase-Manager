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
using DataBaseManagerWPF.Indexes;
using DataBaseManagerWPF.Tables;

namespace DataBaseManagerWPF
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
        }

        private void stck_panel_tables_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var tables = new TablesWindow();
            tables.ShowDialog();
        }

        private void stck_panel_indexes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var indexes = new IndexesWindow();
            indexes.ShowDialog();
        }
    }
}
