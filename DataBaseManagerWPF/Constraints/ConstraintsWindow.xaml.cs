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

namespace DataBaseManagerWPF.Constraints
{
    /// <summary>
    /// Interaction logic for ConstraintsWindow.xaml
    /// </summary>
    public partial class ConstraintsWindow : Window
    {
        private readonly string _projectionQuery;

        public ConstraintsWindow()
        {
            InitializeComponent();
            _projectionQuery = $"SELECT TABSCHEMA, CONSTNAME, TABNAME, TYPE FROM SYSCAT.TABCONST WHERE TABSCHEMA = '{Connection.CurrentSchema}'";
        }

        private void btn_create_constraint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_drop_constraint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_generate_ddl_constraint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.RefreshDataGrid(dataGridConstraints, _projectionQuery);
        }
    }
}
