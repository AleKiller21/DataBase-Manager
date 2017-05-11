using System;
using System.Data;
using System.Windows;
using System.Windows.Documents;
using DataBaseLayer;
using IBM.Data.DB2;

namespace DataBaseManagerWPF
{
    /// <summary>
    /// Interaction logic for SqlEditorWindow.xaml
    /// </summary>
    public partial class SqlEditorWindow : Window
    {
        public SqlEditorWindow()
        {
            InitializeComponent();
        }

        public SqlEditorWindow(string query)
        {
            InitializeComponent();

            txt_command.Text = query;
        }

        private void btn_run_Click(object sender, RoutedEventArgs e)
        {
            var command = new DB2Command(txt_command.Text, Connection.CurrentConnection);

            if (IsProjection())
            {
                try
                {
                    var data = new DataTable();
                    data.Load(command.ExecuteReader());
                    DataGridResult.ItemsSource = data.DefaultView;
                    txt_status.Text = "The command has finished successfully.";
                }
                catch (Exception exception)
                {
                    txt_status.Text = exception.Message;
                }
            }

            else
            {
                try
                {
                    command.ExecuteNonQuery();
                    txt_status.Text = "The command has finished successfully.";
                }
                catch (Exception exception)
                {
                    txt_status.Text = exception.Message;
                }
            }
        }

        private bool IsProjection()
        {
            var projection = txt_command.Text.Substring(0, 6);
            return projection.Equals("select", StringComparison.OrdinalIgnoreCase);
        }
    }
}
