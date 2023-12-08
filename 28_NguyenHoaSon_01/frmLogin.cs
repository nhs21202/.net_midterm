using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _28_NguyenHoaSon_01
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string username = "";
        public string password = "";
        public bool IsValidUser(string user, string pass)
        {
            bool isValid = false;

            string strCommand = "Select * from Customers where EmailAddress = @email and Password = @password";
            string strconnectstring = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"].ToString();

            SqlConnection myConnection = new SqlConnection(strconnectstring);
            myConnection.Open();
            SqlCommand sqlCmd = new SqlCommand(strCommand, myConnection);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            sqlCmd.Parameters.AddWithValue("@email", user);
            sqlCmd.Parameters.AddWithValue("@password", pass);
            DataSet ds2 = new DataSet();
            da.Fill(ds2, "UserAccount");
            myConnection.Close();

            if (ds2.Tables["UserAccount"].Rows.Count > 0)
            {
                isValid = true;
            }

            return isValid;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (this.IsValidUser(txtEmail.Text, txtPass.Text) == false)
            {
                MessageBox.Show("Invalid Username or Password!", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.password = this.txtPass.Text;
                this.username = this.txtEmail.Text;
                MessageBox.Show("Login Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (this.MdiParent is _28_NguyenHoaSon_01)
                {
                    ((_28_NguyenHoaSon_01)this.MdiParent).Text = "User: " + this.password + " - Password: " + this.username;
                }
                this.Close();


            }
        }

        private void cbbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbbShowPassword.Checked == true)
            {
                this.txtPass.UseSystemPasswordChar = false;

            }
            else
            {
                this.txtPass.PasswordChar = '•';
                this.txtPass.UseSystemPasswordChar = true;
            }
        }
    }
}
