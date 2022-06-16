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
    public partial class ViewReservations : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        int uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("UserLogin.aspx");
                }
                else
                {
                    updateRepeater1();
                }
            }

        }

        private void updateRepeater1()
        {
            uid = Convert.ToInt32(Session["UserId"].ToString());

            SqlConnection con = new SqlConnection(CS);
            SqlDataAdapter da = new SqlDataAdapter("sp_veiwReservations", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.AddWithValue("@UserId", uid);

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count <= 0)
            {
                lblHeader.Text = "You do not have any upcomming reservations at the moment";
            }

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "delete":

                    uid = Convert.ToInt32(Session["UserId"].ToString());

                    DateTime dtDate = DateTime.ParseExact(((Label)e.Item.FindControl("lblDate")).Text, "dd/MM/yyyy", null);
                    string dtDateString = dtDate.ToString("yyyy-MM-dd");

                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        SqlCommand cmd = new SqlCommand("sp_deleteReservation", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@table", ((Label)e.Item.FindControl("lblTable")).Text);
                        cmd.Parameters.AddWithValue("@userId", uid);
                        cmd.Parameters.AddWithValue("@reservationDate", dtDateString);
                        cmd.Parameters.AddWithValue("@timeIn", ((Label)e.Item.FindControl("lblTimeIn")).Text);
                        cmd.Parameters.AddWithValue("@timeOut", ((Label)e.Item.FindControl("lblTimeOut")).Text);

                        con.Open();

                        cmd.ExecuteNonQuery();

                        updateRepeater1();

                    }

                    break;
            }
        }
    }
}