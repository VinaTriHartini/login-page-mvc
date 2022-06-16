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
    public partial class Restaurant : System.Web.UI.MasterPage
    {
        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                SqlConnection con = new SqlConnection(CS);
                SqlDataAdapter da = new SqlDataAdapter("Select * from users where UserId = " + Convert.ToInt32(Session["UserId"].ToString()), con);
                DataTable dt = new DataTable();

                da.Fill(dt);
                DataRow dr = dt.Rows[0];

                lbtnLogout.Visible = true;
                lbtnLogout.Text = "Logout";
                hlLogin.Visible = false;
                hlRegister.Visible = false;

                lblWelcome.Visible = true;
                lblWelcome.Text = "Logged in as " + dr["Username"];

            }

            else
            {
                hlLogin.Visible = true;
                hlLogin.Text = "Login";
                lbtnLogout.Visible = false;
                hlRegister.Visible = true;
                hlRegister.Text = "Register";

                lblWelcome.Visible = false;
 
            }
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("UserId");
            lbtnLogout.Visible = false;
            lblWelcome.Visible = false;
            hlRegister.Visible = true;
            hlRegister.Text = "Register";
            hlLogin.Visible = true;
            hlLogin.Text = "Login";
        }
    }
}