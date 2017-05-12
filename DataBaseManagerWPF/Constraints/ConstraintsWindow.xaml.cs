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
            var row = dataGridConstraints.SelectedItem as DataRowView;
            if (row == null) return;

            try
            {
                var ddl = ConstraintFactory.GetConstraint(row["TABSCHEMA"].ToString(), row["CONSTNAME"].ToString(),
                    row["TABNAME"].ToString(), row["TYPE"].ToString()).GenerateDDL();

                new SqlEditorWindow(ddl).Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.RefreshDataGrid(dataGridConstraints, _projectionQuery);
        }
    }
}
