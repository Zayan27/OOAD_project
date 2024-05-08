using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        ProgramDataContext db = new ProgramDataContext();
        private void btn_back_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_usrInfo_Click(object sender, EventArgs e)
        {
            var st = new User_Info
            {
                Uname = txtName.Text,
                Uage = int.Parse(txtAge.Text),
                Umail = txtMail.Text,
                B_id = int.Parse(txtBcode.Text),
            };
            db.User_Infos.InsertOnSubmit(st);
            db.SubmitChanges();
            MessageBox.Show("Success");
            LoadData();
        }
        void LoadData()
        {
            var st = from s in db.Branch_infos join o in db.User_Infos on s.B_id equals o.B_id
                     select new { s.B_id, s.B_name, o.Uname, o.Uage , o.Umail};
        }

        private void txtBcode_TextChanged(object sender, EventArgs e)
        {
            if(txtBcode.Text != "")
            {
                var prodExist = from s in db.Branch_infos where s.B_id == int.Parse(txtBcode.Text) select s.B_id;
                if(prodExist.Count() > 0 )
                {
                    var stname = (from s in db.Branch_infos where s.B_id == int.Parse(txtBcode.Text) select s).First();
                    txtBname.Text = stname.B_name;
                }
                else
                {
                    txtBname.Text = "Not Found";
                }
            }
        }
    }
}
