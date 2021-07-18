using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        public const string DATABASE_FILE = "contact.sqlite";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!File.Exists(DATABASE_FILE))
                CreateContact();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        /// <summary>
        /// Create the contact database, including filling with random data
        /// </summary>
        static void CreateContact()
        {
            SQLiteConnection.CreateFile(DATABASE_FILE);
            SQLiteConnection con = new SQLiteConnection($"Data Source={DATABASE_FILE}");
            con.Open();

            //Create all the tables and stuff

            //contact
            string comstr = @"CREATE TABLE Contact (
                                ContactKey int PRIMARY KEY,
                                FirstName string,
                                LastName string,
                                Birthday date)";
            SQLiteCommand com = new SQLiteCommand(comstr, con);
            com.ExecuteNonQuery();

            //phone
            comstr = @"CREATE TABLE Phone (
                        PhoneKey int PRIMARY KEY,
                        ContactKey int,
                        PhoneNumber varchar(20))";
            com = new SQLiteCommand(comstr, con);
            com.ExecuteNonQuery();

            //address
            comstr = @"CREATE TABLE Address (
                        AddressKey int PRIMARY KEY,
                        ContactKey int,
                        Address varchar(100))";
            com = new SQLiteCommand(comstr, con);
            com.ExecuteNonQuery();

            //area lookup
            comstr = @"CREATE TABLE AreaLookup (
                        AreaKey int PRIMARY KEY,
                        AreaNumber varchar(10),
                        AreaName varchar(50))";
            com = new SQLiteCommand(comstr, con);
            com.ExecuteNonQuery();

            //Creates the sample data for the contacts
            var contactData = Contact.CreateContacts();
            for (int i = 0; i < Contact.CONTACT_NUMBER; i++)
            {
                comstr = $"INSERT INTO Contact (ContactKey,FirstName,LastName,Birthday)VALUES ({i},\"{contactData[i].FirstName}\", \"{contactData[i].LastName}\", \"{contactData[i].Birthday}\")";
                com = new SQLiteCommand(comstr, con);
                com.ExecuteNonQuery();

                comstr = $"INSERT INTO Phone (ContactKey, PhoneNumber) VALUES ({i},\"{contactData[i].PhoneNumber}\")";
                com = new SQLiteCommand(comstr, con);
                com.ExecuteNonQuery();

                comstr = $"INSERT INTO Address (ContactKey, Address) VALUES ({i},\"{contactData[i].Address}\")";
                com = new SQLiteCommand(comstr, con);
                com.ExecuteNonQuery();

            }

            con.Close();
        }
    }
}
