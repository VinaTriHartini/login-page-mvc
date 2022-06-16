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
    public partial class UserLogin : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }        

        protected void btnLogin_Click1(object sender, EventArgs e)
        {
            DataTable dtUser = getData("sp_Login");

            if (dtUser.Rows.Count > 0)
            {
                DataRow dr = dtUser.Rows[0];
                Session["UserId"] = Convert.ToInt32(dr["UserId"].ToString());
                Response.Redirect("Menu.aspx");
            }

            else
            {
                lblMsg.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff4d4d");
                lblMsg.Text = "Invalid login please try again"; 
            }
        }

        private DataTable getData(string cmd)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(CS);
            SqlDataAdapter da = new SqlDataAdapter(cmd, con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (cmd.Equals("sp_Login"))
            {
                da.SelectCommand.Parameters.AddWithValue("@username", txtUsername.Text);
                da.SelectCommand.Parameters.AddWithValue("@password", txtPassword.Text);

                da.Fill(dt);
            }
            return dt;
        }
    }
}