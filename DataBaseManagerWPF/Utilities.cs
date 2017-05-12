using System.Windows.Controls;
using DataBaseLayer;

namespace DataBaseManagerWPF
{
    internal static class Utilities
    {
        public static void RefreshDataGrid(DataGrid grid, string query)
        {
            grid.ItemsSource = DBUtilities.ProjectData(query).DefaultView;
        }
    }
}
