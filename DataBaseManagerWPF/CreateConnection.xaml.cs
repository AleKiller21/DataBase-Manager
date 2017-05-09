using System;
using System.Windows;
using DataBaseLayer;

namespace DataBaseManagerWPF
{
    /// <summary>
    /// Interaction logic for CreateConnection.xaml
    /// </summary>
    public partial class CreateConnection : Window
    {
        private string existingConnectionName;

        public CreateConnection(string database, string host, string port, string user, string password, string existingConnectionName)
        {
            InitializeComponent();

            txt_database.Text = database;
            txt_host.Text = host;
            txt_password.Text = password;
            txt_port.Text = port;
            txt_username.Text = user;
            txt_connection_name.Text = existingConnectionName;

            this.existingConnectionName = existingConnectionName;
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
                if (existingConnectionName == "")
                {
                    Connection.CreateConnection(txt_database.Text, txt_host.Text, txt_username.Text, txt_password.Text, txt_port.Text, txt_connection_name.Text);
                    MessageBox.Show(this, "Connection has been created.");
                }
                else
                {
                    Connection.ModifyConnection(txt_database.Text, txt_host.Text, txt_username.Text, txt_password.Text, txt_port.Text, existingConnectionName, txt_connection_name.Text);

                    MessageBox.Show(this, "Connection has been updated.");
                }

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
