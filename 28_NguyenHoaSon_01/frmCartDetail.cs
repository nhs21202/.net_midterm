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
    public partial class frmCartDetail : Form
    {
        public clsCart curCart;
        public bool isAdd = false;
        public frmCartDetail()
        {
            InitializeComponent();
        }
        public frmCartDetail(bool isAdd)
        {
            this.isAdd = isAdd;
            InitializeComponent();
        }
        private void ResetText()
        {
            this.txtRecID.Text = "";
            this.txtProductID.Text = "";
            this.txtQuantity.Text = "";
            this.txtCartID.Text = "";
            this.txtDate.Text = "";
            this.txtNote.Text = "";
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AddCart()
        {
            try
            {
                string strconnectstring = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"].ToString();
                string strCommand = "Insert into ShoppingCart (CartID, Quantity, ProductID, DateCreated, Note) values(@cartid, @quant, @productid, @date, @note)";
                SqlConnection myConnection = new SqlConnection(strconnectstring);
                myConnection.Open();
                SqlCommand sqlCmd = new SqlCommand(strCommand, myConnection);

                sqlCmd.Parameters.AddWithValue("@cartid", this.txtCartID.Text);
                sqlCmd.Parameters.AddWithValue("@quant", this.txtQuantity.Text);
                sqlCmd.Parameters.AddWithValue("@productid", this.txtProductID.Text);
                sqlCmd.Parameters.AddWithValue("@date", this.txtDate.Text);
                sqlCmd.Parameters.AddWithValue("@note", this.txtNote.Text);
                sqlCmd.ExecuteNonQuery();
                myConnection.Close();

            }


            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex.Message.ToString());

            }
        }
        private void EditCart()
        {
            try
            {
                string strconnectstring = System.Configuration.ConfigurationSettings.AppSettings["MyConnectString"].ToString();
                string strCommand = "Update ShoppingCart set CartID = @cartid, Quantity = @quant, ProductID = @productid, DateCreated = @date, Note = @note where RecordID = @recid";
                SqlConnection myConnection = new SqlConnection(strconnectstring);
                myConnection.Open();
                SqlCommand sqlCmd = new SqlCommand(strCommand, myConnection);
                sqlCmd.Parameters.AddWithValue("@cartid", this.txtCartID.Text);
                sqlCmd.Parameters.AddWithValue("@quant", this.txtQuantity.Text);
                sqlCmd.Parameters.AddWithValue("@productid", this.txtProductID.Text);
                sqlCmd.Parameters.AddWithValue("@date", this.txtDate.Text);
                sqlCmd.Parameters.AddWithValue("@note", this.txtNote.Text);

                sqlCmd.Parameters.AddWithValue("@recid", this.curCart.recordID.ToString());

                sqlCmd.ExecuteNonQuery();
                myConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: " + ex.Message.ToString());

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isAdd)
            {
                AddCart();
                DialogResult dr = MessageBox.Show("Bạn có muốn thêm tiếp không?", "Tiếp tục", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {

                    ResetText();
                }
                else this.Close();
            }
            else
            {
                EditCart();
                MessageBox.Show("Sửa thành công!");
                this.Close();
            }
        }

        private void frmCartDetail_Load(object sender, EventArgs e)
        {
            this.txtRecID.Enabled = false;
            if (isAdd)
            {
                this.lblHead.Text = "Add new for Cart";
            }
            else
            {
                this.txtRecID.Text = this.curCart.recordID.ToString();
                this.txtProductID.Text = this.curCart.productID.ToString();
                this.txtQuantity.Text = this.curCart.quantity.ToString();
                this.txtCartID.Text = this.curCart.cartID.ToString();
                this.txtDate.Text = this.curCart.datecreated.ToString();
                this.txtNote.Text = this.curCart.note.ToString();
            }
        }
    }
}
