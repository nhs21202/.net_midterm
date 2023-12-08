using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _28_NguyenHoaSon_01
{
    public class clsCart
    {
        public int recordID = 0;
        public int productID = 0;
        public string cartID = "";
        public int quantity = 0;
        public string datecreated = "";
        public string note = "";

        public clsCart() { }
        public clsCart(int recordID, int productID, string cartID, string datecreated, int quantity, string note)
        {
             this.cartID = cartID;
             this.recordID = recordID;
            this.productID = productID;
            this.quantity = quantity;
            this.datecreated = datecreated;
            this.note = note;
        }
        public override string ToString()
        {
            return "record ID: " + this.recordID + "/n product ID: " + this.productID + "/n cart ID: " + this.cartID +
                "/nQuantity: " + this.quantity + "/nDate created: " + this.datecreated + "/nNote: "+ this.note;
        }
    }
}
