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
    public partial class frmShoppingCart : Form
    {
        public clsCart curCart;
        public bool isAdd = false;
        public bool isSearch = false;
        public frmShoppingCart()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strconnectstring = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"].ToString();
                string strSearch = this.txtSearch1.Text;
                string strSearch2 = this.txtSearch2.Text;
                string strCommand = "Select * from ShoppingCart where Quantity between " + strSearch + " AND " + strSearch2;
                SqlConnection myConnection = new SqlConnection(strconnectstring);
                myConnection.Open();
                SqlCommand sqlCmd = new SqlCommand(strCommand, myConnection);
                if (!isSearch)
                {
                    MessageBox.Show("Kết nối CSDL thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

                da.Fill(ds, "SonCartList");
                this.dgvCart.DataSource = ds;
                this.dgvCart.DataMember = "SonCartList";
                isSearch = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex.Message.ToString());
            }
        }

        private void dgvCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                DataGridViewRow dr = dgvCart.Rows[e.RowIndex];
                if (dr != null)
                {
                    this.curCart = new clsCart();
                    this.curCart.recordID = int.Parse(dr.Cells["RecordID"].Value.ToString());
                    this.curCart.productID = int.Parse(dr.Cells["ProductID"].Value.ToString());
                    this.curCart.quantity = int.Parse(dr.Cells["Quantity"].Value.ToString());
                    this.curCart.cartID = dr.Cells["CartID"].Value.ToString();
                    this.curCart.datecreated = dr.Cells["DateCreated"].Value.ToString();
                    this.curCart.note = dr.Cells["Note"].Value.ToString();

                    this.txtRecID.Text = this.curCart.recordID.ToString();
                    this.txtProductID.Text = this.curCart.productID.ToString();
                    this.txtQuantity.Text = this.curCart.quantity.ToString();
                    this.txtCartID.Text = this.curCart.cartID.ToString();
                    this.txtDate.Text = this.curCart.datecreated.ToString();
                    this.txtNote.Text = this.curCart.note.ToString();
                }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCartDetail frm = new frmCartDetail(true);
            frm.ShowDialog();
            frmShoppingCart_Load(sender, e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            frmCartDetail frm = new frmCartDetail(false);
            frm.curCart = this.curCart;
                frm.ShowDialog();
            frmShoppingCart_Load(sender, e);

        }

        private void frmShoppingCart_Load(object sender, EventArgs e)
        {
            if (isSearch)
            {
                string strconnectstring = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"].ToString();
                string strSearch = this.txtSearch1.Text;
                string strSearch2 = this.txtSearch2.Text;
                string strCommand = "Select * from ShoppingCart";
                SqlConnection myConnection = new SqlConnection(strconnectstring);
                myConnection.Open();
                SqlCommand sqlCmd = new SqlCommand(strCommand, myConnection);
                if (!isSearch)
                {
                    MessageBox.Show("Kết nối CSDL thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

                da.Fill(ds, "SonCartList");
                this.dgvCart.DataSource = ds;
                this.dgvCart.DataMember = "SonCartList";
            }
        }

        private void DeleteCart()
        {
            string strconnectstring = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"].ToString();
            string strCommand = "Delete from ShoppingCart where RecordID = " + this.curCart.recordID;
            SqlConnection myConnection = new SqlConnection(strconnectstring);
            myConnection.Open();
            SqlCommand sqlCmd = new SqlCommand(strCommand, myConnection);
            sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa cart  " + this.curCart.recordID.ToString() + "?", "Xóa review", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    DeleteCart();
                    frmShoppingCart_Load(sender, e);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error occured: " + ex.Message.ToString());
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            string strCommand = "Select * from ShoppingCart where Quantity between " + this.txtSearch1.Text + " AND " + this.txtSearch2.Text;
            frmReport frm = new frmReport(strCommand);
            frm.ShowDialog();
        }
    }
}
