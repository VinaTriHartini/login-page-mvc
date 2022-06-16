using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace RestaurantManagementSystem
{
    public partial class UserRegistration : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == txtConfirmPassword.Text)
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("sp_Register", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text); 
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                    con.Open();
                    cmd.ExecuteNonQuery(); 

                    lblMsg.BackColor = System.Drawing.ColorTranslator.FromHtml("#4dff4d");
                    lblMsg.Text = "Congatulations " + txtUsername.Text.ToString() + " You have been registered";
                }

            }
            else 
            {
                lblMsg.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff4d4d");
                lblMsg.Text = "Please make sure that the two passwords match";
            }
        }
    }
}