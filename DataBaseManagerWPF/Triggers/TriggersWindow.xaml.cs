using System;
using System.Data;
using System.Windows;
using DataBaseLayer;

namespace DataBaseManagerWPF.Triggers
{
    /// <summary>
    /// Interaction logic for TriggersWindow.xaml
    /// </summary>
    public partial class TriggersWindow : Window
    {
        private readonly string _projectionQuery;
        public TriggersWindow()
        {
            InitializeComponent();
            _projectionQuery = $"SELECT TRIGSCHEMA, TRIGNAME, TABNAME, TRIGTIME, TRIGEVENT FROM SYSCAT.TRIGGERS WHERE TRIGSCHEMA = '{Connection.CurrentSchema}'";
        }

        private void btn_create_trigger_Click(object sender, RoutedEventArgs e)
        {
            new SqlEditorWindow(DataBaseLayer.Trigger.GenerateCreateTemplate()).Show();
        }

        private void btn_drop_trigger_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_generate_ddl_trigger_Click(object sender, RoutedEventArgs e)
        {
            var row = dataGridTriggers.SelectedItem as DataRowView;
            if(row == null) return;

            try
            {
                new SqlEditorWindow(DataBaseLayer.Trigger.GenerateDDL(row["TRIGSCHEMA"].ToString(), row["TRIGNAME"].ToString())).Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.RefreshDataGrid(dataGridTriggers, _projectionQuery);
        }
    }
}
