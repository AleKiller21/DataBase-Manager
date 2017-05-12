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

namespace DataBaseManagerWPF.Routines
{
    /// <summary>
    /// Interaction logic for RoutineTypeWindow.xaml
    /// </summary>
    public partial class RoutineTypeWindow : Window
    {
        public RoutineTypeWindow()
        {
            InitializeComponent();
        }

        private void btn_create_routine_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_routine_type.SelectedIndex == 0) new SqlEditorWindow(GenerateFunctionDDL()).Show();
            else new SqlEditorWindow(GenerateProcedureDDL()).Show();

            Close();
        }

        private string GenerateFunctionDDL()
        {
            return @"CREATE OR REPLACE FUNCTION <NAME> ()
	RETURNS INTEGER
	NO EXTERNAL ACTION
F1: BEGIN ATOMIC
	RETURN 
  	-- #######################################################################
	-- # Replace the SQL statement with your statement.
	-- #  Note: Be sure to end statements with the terminator character (usually ';')
	-- #
	-- # The example SQL statement SELECT COUNT(*) FROM SYSIBM.SYSTABLES
	-- # returns the count of tables in SYSIBM.SYSTABLES 
	-- ######################################################################
	SELECT COUNT(*) FROM SYSIBM.SYSTABLES;
END";
        }

        private string GenerateProcedureDDL()
        {
            return @"CREATE OR REPLACE PROCEDURE <NAME> (IN VARNAME VARCHAR(128), OUT VARCOUNT INTEGER)
P1: BEGIN
	-- #######################################################################
	-- # Returns count of tables created by BLUADMIN and like VARNAME
	-- #######################################################################
	SELECT COUNT(*) INTO VARCOUNT FROM SYSIBM.SYSTABLES 
		WHERE CREATOR = 'BLUADMIN' AND NAME LIKE VARNAME;
END P1";
        }
    }
}
