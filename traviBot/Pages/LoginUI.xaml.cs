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
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace traviBot.Pages
{
    /// <summary>
    /// Interaction logic for LoginUI.xaml
    /// </summary>
    public partial class LoginUI : UserControl
    {
                public string UserName { get; set; }
        public string Password { get; set; }
        public string server { get; set; }
        private bool databaseExists;
        private int userId = 1;


        public LoginUI()
        {
            InitializeComponent();

            //TODO: Server list.
            serverBox.Items.Add("ts3.travian.si");

            string conString;

            databaseExists = File.Exists("TraviBot.sqlite");

            if (!databaseExists)
                conString = "Data Source=TraviBot.sqlite;Version=3;New=True";
            else
                conString = "Data Source=TraviBot.sqlite;Version=3;";

            try
            {
                SQLiteConnection m_dbConnection = new SQLiteConnection(conString);

                m_dbConnection.Open();
                if (!databaseExists)
                {
                    SQLiteCommand createSQL = new SQLiteCommand("CREATE TABLE 'accounts' ('id' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 'username' TEXT, 'password' TEXT, 'server' TEXT);" +
                    "CREATE TABLE 'buildingQueue' ('id' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 'buildingId' INTEGER, 'dateEnd' TIMESTAMP);" +
                    "CREATE TABLE 'settings' ('id' INTEGER PRIMARY KEY, 'accountId' INTEGER REFERENCES 'accounts' ('id'), 'Name' TEXT, 'Value' TEXT);" +
                    "CREATE TABLE 'trainingQueue' ('id' INTEGER PRIMARY KEY AUTOINCREMENT, 'objectId' INTEGER, 'objectType' INTEGER, 'dateEnd' TIMESTAMP);", m_dbConnection);
                    createSQL.ExecuteNonQuery();
                }
                else
                {
                    SQLiteCommand selectSQL = new SQLiteCommand("SELECT username, password FROM accounts WHERE id=@UserID;", m_dbConnection);
                    selectSQL.Parameters.AddWithValue("UserID", this.userId);
                    SQLiteDataReader reader = selectSQL.ExecuteReader();
                    while (reader.Read())
                    {
                        userNameBox.Text = reader["username"].ToString();
                        passwordBox.Password = reader["password"].ToString();
                    }
                    m_dbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(ex.Message);
            }
            
        }

        private void OpenMainWindow()
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Account.login.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.UserName = userNameBox.Text;
            this.Password = passwordBox.Password;

            if (!databaseExists)
            {
                try
                {
                    SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=TraviBot.sqlite;Version=3;");

                    m_dbConnection.Open();
                    SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO accounts (username, password) VALUES (@username, @password)", m_dbConnection);
                    insertSQL.Parameters.AddWithValue("username", this.UserName);
                    insertSQL.Parameters.AddWithValue("password", this.Password);
                    insertSQL.ExecuteNonQuery();
                    m_dbConnection.Close();
                }
                catch (Exception ex) { 
                    MessageBoxResult result = MessageBox.Show(ex.Message);
                }
            }

            Account.username = this.UserName;
            Account.password = this.Password;
            Account.Start();

            OpenMainWindow();
        }
    }
}
