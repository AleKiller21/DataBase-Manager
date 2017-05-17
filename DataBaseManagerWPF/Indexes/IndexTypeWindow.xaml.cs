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

namespace DataBaseManagerWPF.Indexes
{
    /// <summary>
    /// Interaction logic for IndexTypeWindow.xaml
    /// </summary>
    public partial class IndexTypeWindow : Window
    {
        public IndexTypeWindow()
        {
            InitializeComponent();
        }

        private void btn_create_index_Click(object sender, RoutedEventArgs e)
        {
            var ddl = "";

            switch (cmb_index_type.SelectedIndex)
            {
                case 0:
                    ddl = $@"ALTER TABLE {Connection.CurrentSchema}.<TABLE NAME>
ADD CONSTRAINT <CONSTRAINT NAME> PRIMARY KEY (<COLUMN>)";
                    break;
                case 1:
                    ddl = $"CREATE UNIQUE {Connection.CurrentSchema}.INDEX<NAME> ON {Connection.CurrentSchema}.<TABLE NAME> (< COLUMNS >);";
                    break;
                default:
                    ddl = $"CREATE {Connection.CurrentSchema}.INDEX<NAME> ON {Connection.CurrentSchema}.<TABLE NAME> (< COLUMNS >);";
                    break;
            }


            var sqlEditor = new SqlEditorWindow(ddl);
            sqlEditor.Show();
            Close();
        }
    }
}
