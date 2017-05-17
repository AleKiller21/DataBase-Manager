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

namespace DataBaseManagerWPF.Users
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        private readonly string _projectionQuery;

        public UsersWindow()
        {
            InitializeComponent();
            _projectionQuery = $"SELECT AUTHID FROM SYSIBMADM.AUTHORIZATIONIDS WHERE AUTHIDTYPE = 'U'";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.RefreshDataGrid(dataUsersGrid, _projectionQuery);
        }
    }
}
