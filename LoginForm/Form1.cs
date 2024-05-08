using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Win32;

namespace LoginForm
{
    public partial class Form1 : Form
    {
        private bool hasRunBefore = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        

        
        

        private void login_btn_Click(object sender, EventArgs e)
        {
            String username, userpassword, serverName, dbName;
            serverName = textbox_serverip.Text;
            dbName = textbox_dbname.Text;
            SaveRunBeforeFlag();

            SaveToRegistry("ServerName", serverName);
            SaveToRegistry("DatabaseName", dbName);

            string R_servername = ReadRegistry("ServerName");
            string R_dbname = ReadRegistry("DatabaseName");

            SqlConnection sqlConnection = new SqlConnection($"Data Source={R_servername};Initial Catalog={R_dbname};Integrated Security=True;Encrypt=False");
            
            
            username = textbox_userid.Text;
            userpassword = textbox_userpass.Text;
            try {
                String querry = "SELECT * FROM Login_new WHERE username = '" + textbox_userid.Text + "' AND password = '" + textbox_userpass.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(querry,sqlConnection);

                DataTable dt = new DataTable();
                sda.Fill(dt);

                if(dt.Rows.Count > 0 )
                {
                    username = textbox_userid.Text;
                    userpassword = textbox_userpass.Text;

                    //new page loads

                    Menu form2 = new Menu();
                    form2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Login Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textbox_userid.Clear();
                    textbox_userpass.Clear();

                    textbox_userid.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally { sqlConnection.Close(); }
           
        }

        private bool CheckRunBeforeFlag()
        {
            
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("ServerValues"))
            {
                if (key != null)
                {
                    object value = key.GetValue("HasRunBefore");
                    if (value != null && value.ToString() == "1")
                    {
                        
                        return true; 
                    }
                }
            }
            return false; 
        }
        private void UpdateFormLayout()
        {
            if (!hasRunBefore)
            {
                
                textbox_serverip.Visible = false;
                textbox_dbname.Visible = false;
                Server_ip.Visible = false;
                db_name.Visible = false;
            }
            else
            {
                textbox_serverip.Visible = true;
                textbox_dbname.Visible = true;
                Server_ip.Visible = true;
                db_name.Visible = true;
            }
        }
        private void SaveRunBeforeFlag()
        {
            
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("ServerValues"))
            {
                key.SetValue("HasRunBefore", "1", RegistryValueKind.DWord);
            }
        }

        private void SaveToRegistry(string keyName, string value)
        {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey("ServerValues");
            if (regKey == null)
            {
                regKey.SetValue(keyName, value, RegistryValueKind.String);
               
            }
        }

        private string ReadRegistry(string keyName)
        {
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey("ServerValues");

            return regkey.GetValue(keyName).ToString();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            hasRunBefore = CheckRunBeforeFlag();

            UpdateFormLayout();
        }

        private void clr_btn_Click(object sender, EventArgs e)
        {
            textbox_userid.Clear();
            textbox_userpass.Clear();

            textbox_userid.Focus();
        }

        private void exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
