using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private const string SEARCH_TEXT = "Search...";
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = SEARCH_TEXT;
        }

        private void textBox1_FocusEnter(object sender, EventArgs e)
        {
            if (textBox1.Text == SEARCH_TEXT)
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_FocusLeave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = SEARCH_TEXT;
            }
        }

        Contact[] SelectStatement()
        {
            SQLiteConnection con = new SQLiteConnection($"Data Source={Program.DATABASE_FILE}");
            con.Open();
            int radio = getRadio();
            string command = "SELECT * FROM Contact LEFT JOIN Address ON Contact.ContactKey == Address.ContactKey LEFT JOIN Phone ON Contact.ContactKey == Phone.ContactKey LEFT JOIN AreaLookup ON PhoneNumber == AreaNumber + '-XXX-XXXX'";
            string whereStatement = "";
            switch (radio)
            {
                case 1:
                    whereStatement = $"WHERE FirstName LIKE '%{getSearchQuery()}%' OR LastName LIKE '%{getSearchQuery()}%'";
                    break;
                case 2:
                    //command += "LEFT JOIN Address ON Contact.ContactKey == Address.ContactKey ";
                    whereStatement = $"WHERE Address LIKE '%{getSearchQuery()}%'";
                    break;
                case 3:
                    //command += "LEFT JOIN Phone ON Contact.ContactKey == Phone.ContactKey ";
                    whereStatement = $"WHERE PhoneNumber LIKE '%{getSearchQuery()}%'";
                    break;
                case 4:
                    //command +=
                    //    $"LEFT JOIN Phone ON Contact.ContactKey == Phone.ContactKey LEFT JOIN AreaLookup ON PhoneNumber == AreaNumber + '-XXX-XXXX'";
                    whereStatement = $"WHERE AreaName LIKE '%{getSearchQuery()}%'";
                    break;
                case 5:
                    whereStatement = $"WHERE Birthday LIKE '%{getSearchQuery()}%'";
                    break;
            }

            command += whereStatement + " ORDER BY FirstName";
            SQLiteCommand com = new SQLiteCommand(command, con);
            var reader = com.ExecuteReader();
            return Contact.ReadContacts(reader, radio);
        }

        private string getSearchQuery()
        {
            if (textBox1.Text == SEARCH_TEXT)
                return "";
            return textBox1.Text;
        }

        private int getRadio()
        {
            if (radioButton1.Checked)
                return 1;
            if (radioButton2.Checked)
                return 2;
            if (radioButton3.Checked)
                return 3;
            if (radioButton4.Checked)
                return 4;
            return 5;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) =>
            SelectionChanged(1);

        private void radioButton2_CheckedChanged(object sender, EventArgs e) =>
            SelectionChanged(2);

        private void radioButton3_CheckedChanged(object sender, EventArgs e) =>
            SelectionChanged(3);

        private void radioButton4_CheckedChanged(object sender, EventArgs e) =>
            SelectionChanged(4);

        private void radioButton5_CheckedChanged(object sender, EventArgs e) =>
            SelectionChanged(5);

        private void SelectionChanged(int skip)
        {
            if (skip != 1)
                radioButton1.Checked = false;
            if (skip != 2)
                radioButton2.Checked = false;
            if (skip != 3)
                radioButton3.Checked = false;
            if (skip != 4)
                radioButton4.Checked = false;
            if (skip != 5)
                radioButton5.Checked = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            var contacts = SelectStatement();
            foreach (var contact in contacts)
            {
                listBox1.Items.Add(contact.ToString());
            }
        }
    }
}
