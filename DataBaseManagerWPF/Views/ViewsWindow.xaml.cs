using System.Data;
using System.Windows;
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
            new SqlEditorWindow("CREATE OR REPLACE VIEW <VIEW_NAME> AS\n<PROJECTION>").Show();
        }

        private void btn_drop_view_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_generate_ddl_view_Click(object sender, RoutedEventArgs e)
        {
            var row = dataGridViews.SelectedItem as DataRowView;
            if(row == null) return;

            new SqlEditorWindow(View.GenerateDDL(row["VIEWSCHEMA"].ToString(), row["VIEWNAME"].ToString())).Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.RefreshDataGrid(dataGridViews, _projectionQuery);
        }
    }
}
