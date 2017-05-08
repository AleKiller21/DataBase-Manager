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

namespace DataBaseManagerWPF
{
    /// <summary>
    /// Interaction logic for CreateConnection.xaml
    /// </summary>
    public partial class CreateConnection : Window
    {
        public CreateConnection()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_finish_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfFieldsAreEmpty())
            {
                MessageBox.Show(this, "Empty fields detected.");
                return;
            }

            try
            {
                Connection.CreateConnection(txt_database.Text, txt_host.Text, txt_username.Text, txt_password.Text, txt_port.Text);
                MessageBox.Show(this, "Connection has been created.");
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message);
            }
        }

        private bool CheckIfFieldsAreEmpty()
        {
            return txt_database.Text == "" || txt_host.Text == "" || txt_password.Text == ""
                   || txt_port.Text == "" || txt_username.Text == "";
        }
    }
}
