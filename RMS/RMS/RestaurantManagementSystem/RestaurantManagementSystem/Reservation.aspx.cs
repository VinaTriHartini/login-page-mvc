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
    public partial class Reservation : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        int Uid;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection(CS);
                SqlDataAdapter da = new SqlDataAdapter("select * from tblTable", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                DropDownList1.DataSource = dt;
                DropDownList1.DataTextField = "NoOfPeople";
                DropDownList1.DataValueField = "TableId";
                DropDownList1.DataBind();

                ListItem liFirst = new ListItem("Select a table", "-1");
                DropDownList1.Items.Insert(0, liFirst);

                RandomTable();
            }

        }

        private void RandomTable()
        {
            string[] tbl = new string[5];

            tbl[0] = "Table for 2"; tbl[1] = "Table for 4"; tbl[2] = "Table for 6"; tbl[3] = "Table for 10"; tbl[4] = "Table for 12";

            Random random = new Random();
            int num = random.Next(5);

            tblImage.ImageUrl = "~/images/" + tbl[num] + ".jpg";
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            lblMessage.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            lblMessage.Text = "Bookings";

            DataTable dtReset = new DataTable();
            rptAvailableTimes.DataSource = dtReset;
            rptAvailableTimes.DataBind();

            if (DropDownList1.SelectedValue == "-1")
            {
                RandomTable();
            }
            else 
            {
                string str = DropDownList1.SelectedItem.ToString();
                tblImage.ImageUrl = "~/images/" + str + ".jpg";
            }
        }

        protected void btnCheckAvailability_Click(object sender, EventArgs e)
        {
            string sdate = Calendar1.SelectedDate.ToString("yyyy-MM-dd");

            if (sdate != null && !DropDownList1.SelectedValue.ToString().Equals("-1"))
            {
                if (ddlTimeIn.SelectedValue.ToString().Equals(-1) || (ddlTimeOut.SelectedValue.ToString().Equals("-1")))
                {
                    lblMessage.BackColor = System.Drawing.ColorTranslator.FromHtml("#4dff4d");
                    lblMessage.Text = "Please look in the following list and pick the available time";
                    lblDate.Text = sdate + " : ";
                    lblTable.Text = DropDownList1.SelectedItem.ToString();

                    DisplayAvailableTimeForChasenTable(DropDownList1.SelectedItem.ToString(), sdate);
                }

                else
                {
                    string ampmIn = ddlTimeIn.SelectedItem.ToString().Substring(ddlTimeIn.SelectedItem.ToString().Length - 2);
                    string ampmOut = ddlTimeOut.SelectedItem.ToString().Substring(ddlTimeOut.SelectedItem.ToString().Length - 2);

                    string ddltin = convertTo24hr(ddlTimeIn.SelectedItem.ToString().Substring(0, ddlTimeIn.SelectedItem.ToString().Length - 3), ampmIn);
                    string ddltout = convertTo24hr(ddlTimeOut.SelectedItem.ToString().Substring(0, ddlTimeOut.SelectedItem.ToString().Length - 3), ampmOut);

                    SqlConnection con = new SqlConnection(CS);
                    SqlDataAdapter da = new SqlDataAdapter("sp_checkAvailability", con);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@tin", ddltin);
                    da.SelectCommand.Parameters.AddWithValue("@tout", ddltout);
                    da.SelectCommand.Parameters.AddWithValue("@table", DropDownList1.SelectedItem.ToString());
                    da.SelectCommand.Parameters.AddWithValue("@RDate", sdate);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        lblMessage.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff4d4d");
                        lblMessage.Text = "The selected Reservation is not available at the moment <br/> Please look on the following list for available times";
                        lblDate.Text = sdate + " : ";
                        lblTable.Text = DropDownList1.SelectedItem.ToString();

                        DisplayAvailableTimeForChasenTable(DropDownList1.SelectedItem.ToString(), sdate);

                    }

                    else
                    {
                        lblMessage.BackColor = System.Drawing.ColorTranslator.FromHtml("#4dff4d");
                        lblMessage.Text = "Table is available for the selected time <br/> You can go ahead and make your reservation";
                        lblDate.Text = sdate + " : ";
                        lblTable.Text = DropDownList1.SelectedItem.ToString();
                    }

                }

            }
            else 
            {
                lblDate.Text = "";
                lblTable.Text = "";
                lblMessage.BackColor = System.Drawing.ColorTranslator.FromHtml("#4dff4d");
                lblMessage.Text = "Please select the table and the date";
            }
        }

        private void DisplayAvailableTimeForChasenTable(string tbl, string sdate)
        {
            SqlConnection con = new SqlConnection(CS);

            SqlDataAdapter da = new SqlDataAdapter("sp_checkAvailabilityUsingTable", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.AddWithValue("@table", tbl);
            da.SelectCommand.Parameters.AddWithValue("@sdate", sdate);

            DataTable dt = new DataTable();
            da.Fill(dt);

            List<string> AvailableTimes = new List<string>();
            List<string> TimeStatus = new List<string>();
            int count = 7;

            for (int i = 7; i <=19; i++ )
            {
                TimeStatus.Add("Available");

                if (i <= 12)
                {
                    if (count < 10)
                    {
                        AvailableTimes.Add("0" + count + ":00:00");
                    }

                    else
                    {
                        AvailableTimes.Add(count + ":00:00");
                    }


                    if (i == 12)
                    {
                        count = 0; 
                    }
                }
                else 
                {
                    AvailableTimes.Add("0" + count + ":00:00");
                }
                count++;
            }

            foreach (DataRow dr in dt.Rows)
            {
                string tin = convertTo12hr(dr["Time_in"].ToString());
                string tout = convertTo12hr(dr["Time_out"].ToString());

                TimeStatus.Insert(AvailableTimes.IndexOf(tin), "Reserved");
                TimeStatus.RemoveAt(AvailableTimes.IndexOf(tin) + 1);
            }

            DataTable dt2 = new DataTable();
            dt2.Columns.AddRange(new DataColumn[2] {new DataColumn("RTimes"), new DataColumn("TStatus") });

            for (int i = 0; i < AvailableTimes.Count; i++ )
            {
                string[] arr = new string[2];

                if (i < AvailableTimes.Count - 1)
                {
                    string tempTimes = AvailableTimes[i].Substring(0, (AvailableTimes[i].Length)-3) + " to " + AvailableTimes[i + 1].Substring(0, (AvailableTimes[i].Length)-3);

                    arr[0] = tempTimes;
                    arr[1] = TimeStatus[i].ToString();
                    dt2.Rows.Add(arr);
                }
            }
            rptAvailableTimes.DataSource = dt2;
            rptAvailableTimes.DataBind();
        }

        private string convertTo12hr(string s)
        {
            string rv;
            int hr24 = Convert.ToInt32(s.Substring(0, s.IndexOf(":")));

            switch (hr24)
            {
                case (13):
                    hr24 = 01;
                    break;
                case (14):
                    hr24 = 02;
                    break;
                case (15):
                    hr24 = 03;
                    break;
                case (16):
                    hr24 = 04;
                    break;
                case (17):
                    hr24 = 05;
                    break;
                case (18):
                    hr24 = 06;
                    break;
                case (19):
                    hr24 = 07;
                    break;
            }

            if(hr24 == 12 || hr24 == 11 || hr24 == 10)
            {
                rv = "" + hr24 + ":00:00";
            }
            else
            {
                rv = "0" + hr24 + ":00:00";
            
            }
            return rv;
        }

        private string convertTo24hr(string s, string am)
        {
            string rv;

            int hr12 = Convert.ToInt32(s.Substring(0, s.IndexOf(':')));
            if (am.Equals("pm"))
            {
                switch(hr12)
                {
                    case(01):
                        hr12 = 13;
                        break;
                    case (02):
                        hr12 = 14;
                        break;
                    case (03):
                        hr12 = 15;
                        break;
                    case (04):
                        hr12 = 14;
                        break;
                    case (05):
                        hr12 = 17;
                        break;
                    case (06):
                        hr12 = 18;
                        break;
                    case (07):
                        hr12 = 19;
                        break;
                }
            }

            rv = "" + hr12 + ":00:00";
            return rv;
        }

        protected void rptAvailableTimes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;
                if(row["TStatus"].ToString().Equals("Reserved"))
                {
                    ((Label)e.Item.FindControl("lblTimeStatus")).Text = "<b>Reserved<b>";
                    ((Label)e.Item.FindControl("lblTimeStatus")).BackColor = System.Drawing.ColorTranslator.FromHtml("#ff4d4d");
                }
                else if (row["TStatus"].ToString().Equals("Available"))
                {
                    ((Label)e.Item.FindControl("lblTimeStatus")).BackColor = System.Drawing.ColorTranslator.FromHtml("#77fffa");
                }
            }
        }

        protected void SubmitReservation_Click(object sender, EventArgs e)
        {
            string sdate = Calendar1.SelectedDate.ToString("yyyy-MM-dd");

            if (Session["UserID"] == null)
            {
                Response.Redirect("UserLogin.aspx");
            }

            else 
            {
                Uid = Convert.ToInt32(Session["UserId"]);

                if (Calendar1.SelectedDate.ToString("yyyy-MM-dd") != "0001-01-01" && Calendar1.SelectedDate != null && ddlTimeIn.SelectedValue.ToString() != "-1" && ddlTimeOut.SelectedValue.ToString() != "-1" && DropDownList1.SelectedValue.ToString() != "-1")
                {

                    string ampmIn = ddlTimeIn.SelectedItem.ToString().Substring(ddlTimeIn.SelectedItem.ToString().Length - 2);
                    string ampmOut = ddlTimeOut.SelectedItem.ToString().Substring(ddlTimeOut.SelectedItem.ToString().Length - 2);

                    string ddltin = convertTo24hr(ddlTimeIn.SelectedItem.ToString().Substring(0, ddlTimeIn.SelectedItem.ToString().Length - 3), ampmIn);
                    string ddltout = convertTo24hr(ddlTimeOut.SelectedItem.ToString().Substring(0, ddlTimeOut.SelectedItem.ToString().Length - 3), ampmOut);

                    SqlConnection con = new SqlConnection(CS);
                    SqlDataAdapter da = new SqlDataAdapter("sp_checkAvailability", con);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@tin", ddltin);
                    da.SelectCommand.Parameters.AddWithValue("@tout", ddltout);
                    da.SelectCommand.Parameters.AddWithValue("@table", DropDownList1.SelectedItem.ToString());
                    da.SelectCommand.Parameters.AddWithValue("@RDate", sdate);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        lblMessage.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff4d4d");
                        lblMessage.Text = "The selected Reservation is not available at the moment <br/> Please look on the following list for available times";
                        lblDate.Text = sdate + " : ";
                        lblTable.Text = DropDownList1.SelectedItem.ToString();

                        DisplayAvailableTimeForChasenTable(DropDownList1.SelectedItem.ToString(), sdate);

                    }

                    else
                    {
                        using (con)
                        {
                            SqlCommand cmd = new SqlCommand("spMakeReservation", con);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Rdate", sdate);
                            cmd.Parameters.AddWithValue("@Rtin", ddlTimeIn.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@Rtout", ddlTimeOut.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@Rtable", DropDownList1.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@RUid", Uid);

                            con.Open();
                            cmd.ExecuteNonQuery();

                        }


                        lblMessage.BackColor = System.Drawing.ColorTranslator.FromHtml("#4dff4d");
                        lblMessage.Text = "Congratulations your reservation has been made successfull. <br/> We are looking forward to having you in our restaurant";
                        DataTable dtReset = new DataTable();
                        rptAvailableTimes.DataSource = dtReset;
                        rptAvailableTimes.DataBind();
                    }
 
                }

                else
                {
                    lblMessage.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff4d4d");
                    lblMessage.Text = "Please select all fields";
                }
            }

        }

        protected void BtnViewAllAvailableTimes_Click(object sender, EventArgs e)
        {
            string sdate = Calendar1.SelectedDate.ToString("yyyy-MM-dd");

            Response.Redirect("ViewAllAvailableTimes.aspx?SelectedDate=" + sdate);
        }

        protected void ViewMyReservations_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewReservations.aspx");
        }

        
    }

   
}