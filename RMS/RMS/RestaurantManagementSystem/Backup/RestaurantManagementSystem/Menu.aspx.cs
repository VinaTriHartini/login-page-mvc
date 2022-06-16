using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace RestaurantManagementSystem
{
    public partial class Menu : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        int UserId;

        double totalPrice = 0;
        double totalPriceRow = 0;
        double Price;
        int quantity;
        int NoOfItems = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(CS);
            SqlDataAdapter da = new SqlDataAdapter("select * from tblMeals", con);

            DataSet ds = new DataSet();

            da.Fill(ds);
            rpt.DataSource = ds;
            rpt.DataBind();

            if(Session["UserId"] != null)
            {
                UserId = Convert.ToInt32(Session["UserId"].ToString());
                calculateUserOrder();
            }
        }

        private void calculateUserOrder()
        {
            SqlConnection con = new SqlConnection(CS);
            SqlDataAdapter da = new SqlDataAdapter("MealIDsAndQuantities", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.AddWithValue("@UserId", UserId);

            DataTable dt = new DataTable();

            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                lblNoOfItems.Text = "0";
                lblTotalPrice.Text = "0";
            }
            else 
            {
                foreach(DataRow dr in dt.Rows)
                {
                    Price = Convert.ToInt32(dr["Price"].ToString());
                    quantity = Convert.ToInt32(dr["quantity"].ToString());

                    totalPriceRow = Price * quantity;
                    totalPrice = totalPrice + totalPriceRow;
                    NoOfItems = NoOfItems + quantity;

                    Price = 0;
                    quantity = 0;
                }
                lblTotalPrice.Text = totalPrice.ToString();
                lblNoOfItems.Text = NoOfItems.ToString();
            }
        }

        protected void btnViewMyOrder_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("UserLogin.aspx");
            }
            else
            {
                Response.Redirect("ViewOrder.aspx");
            }
        }
    }
}