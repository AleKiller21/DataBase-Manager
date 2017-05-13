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
using DataBaseManagerWPF.Constraints;
using DataBaseManagerWPF.Indexes;
using DataBaseManagerWPF.Routines;
using DataBaseManagerWPF.Tables;
using DataBaseManagerWPF.Triggers;
using DataBaseManagerWPF.Users;
using DataBaseManagerWPF.Views;

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
            new TablesWindow().ShowDialog();
        }

        private void stck_panel_indexes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            new IndexesWindow().ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Connection.Disconnect();
        }

        private void stck_panel_routines_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            new RoutinesWindow().ShowDialog();
        }

        private void stck_panel_views_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            new ViewsWindow().ShowDialog();
        }

        private void stck_panel_constraints_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            new ConstraintsWindow().ShowDialog();
        }

        private void stck_panel_triggers_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            new TriggersWindow().ShowDialog();
        }

        private void btn_editor_Click(object sender, RoutedEventArgs e)
        {
            new SqlEditorWindow().Show();
        }

        private void stck_panel_users_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            new UsersWindow().Show();
        }
    }
}
