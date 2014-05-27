using FirstFloor.ModernUI.Windows.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Diagnostics;

namespace traviBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            //using (SQLiteConnection con = new SQLiteConnection(connectionString))
            //{
            //    con.Open();

            //    try
            //    {
            //        insertSQL.ExecuteNonQuery();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception(ex.Message);
            //    }  

            //    insertSQL.CommandText = "SELECT id, username, password FROM accounts;";
            //    insertSQL.Parameters.Clear();
            //    using (SQLiteDataReader rdr = insertSQL.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                MessageBoxResult result = MessageBox.Show(rdr.GetInt32(0) + " "
            //                    + rdr.GetString(1) + " " + rdr.GetInt32(2));
            //            }
            //        }

            //    con.Close();
            //}
        }
    }
}
