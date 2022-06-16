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
    public partial class ViewOrder : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        int uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Menu.aspx");
            }

            else 
            {
                uid = Convert.ToInt32(Session["UserId"].ToString());
                SqlConnection con = new SqlConnection(CS);
                SqlDataAdapter da = new SqlDataAdapter("MealIDsAndQuantities", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@UserId", uid);

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    lblMsg.Text = "You haven't made any purchases yet, please go to menu and select items you wish to purchase";
                    lblOrderMoreFood.Text = "Order Food";
                }
                else
                {
                    lblMsg.Text = "You have ordered the following items: ";
                    lblOrderMoreFood.Text = "Order More Food";

                    rptOrder.DataSource = dt;
                    rptOrder.DataBind();
                }
            }

        }
    }
}