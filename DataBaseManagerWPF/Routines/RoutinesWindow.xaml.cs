using System.Windows;
using DataBaseLayer;

namespace DataBaseManagerWPF.Routines
{
    /// <summary>
    /// Interaction logic for RoutinesWindow.xaml
    /// </summary>
    public partial class RoutinesWindow : Window
    {
        private string _projectionQuery;
        public RoutinesWindow()
        {
            InitializeComponent();
            _projectionQuery =
                $"SELECT ROUTINESCHEMA, ROUTINENAME, ROUTINETYPE FROM SYSCAT.ROUTINES WHERE ROUTINESCHEMA = '{Connection.CurrentSchema}' AND TEXT IS NOT NULL;";
        }

        private void btn_create_routine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_drop_routine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_alter_routine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_generate_ddl_routine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.RefreshDataGrid(dataGridRoutines, _projectionQuery);
        }
    }
}
