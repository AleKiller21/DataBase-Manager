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

namespace DataBaseManagerWPF.Constraints
{
    /// <summary>
    /// Interaction logic for ConstraintTypeWindow.xaml
    /// </summary>
    public partial class ConstraintTypeWindow : Window
    {
        public ConstraintTypeWindow()
        {
            InitializeComponent();
        }

        private void btn_create_constraint_Click(object sender, RoutedEventArgs e)
        {
            switch (cmb_constraint_type.SelectedIndex)
            {
                case 0:
                    new SqlEditorWindow(PrimaryKey.GenerateCreateTemplate()).Show();
                    Close();
                    break;


                case 1:
                    new SqlEditorWindow(ForeignKey.GenerateCreateTemplate()).Show();
                    Close();
                    break;

                case 2:
                    new SqlEditorWindow(Check.GenerateCreateTemplate()).Show();
                    Close();
                    break;
            }
        }
    }
}
