using System;
using System.Windows;
using System.Windows.Controls;
using DataBaseLayer;

namespace DataBaseManagerWPF
{
    internal static class Utilities
    {
        public static void RefreshDataGrid(DataGrid grid, string query)
        {
            try
            {
                grid.ItemsSource = DBUtilities.ProjectData(query).DefaultView;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
