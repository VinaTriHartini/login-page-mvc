using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace RestaurantManagementSystem
{
    public partial class ViewAllAvailableTimes : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            Calendar1.Visible = false;

            if(!IsPostBack)
            {
                if (Request.QueryString["SelectedDate"] == null)
                {
                    Response.Redirect("Reservation.aspx");
                }

                else 
                {
                    string sdate = Request.QueryString["SelectedDate"].ToString();

                    lblDate.Text = sdate;

                    lblTable1.Text = "Table for 2";
                    lblDate1.Text = sdate;

                    lblTable2.Text = "Table for 4";
                    lblDate2.Text = sdate;

                    lblTable3.Text = "Table for 6";
                    lblDate3.Text = sdate;

                    lblTable4.Text = "Table for 10";
                    lblDate4.Text = sdate;

                    lblTable5.Text = "Table for 12";
                    lblDate5.Text = sdate;

                    rptAvailableTimes1.DataSource = DisplayAvailableTimeForChosenTable("Table for 2", sdate);
                    rptAvailableTimes1.DataBind();

                    rptAvailableTimes2.DataSource = DisplayAvailableTimeForChosenTable("Table for 4", sdate);
                    rptAvailableTimes2.DataBind();

                    rptAvailableTimes3.DataSource = DisplayAvailableTimeForChosenTable("Table for 6", sdate);
                    rptAvailableTimes3.DataBind();

                    rptAvailableTimes4.DataSource = DisplayAvailableTimeForChosenTable("Table for 10", sdate);
                    rptAvailableTimes4.DataBind();

                    rptAvailableTimes5.DataSource = DisplayAvailableTimeForChosenTable("Table for 12", sdate);
                    rptAvailableTimes5.DataBind();
                }
            }

        }

        private DataTable DisplayAvailableTimeForChosenTable(string tbl, string sdate)
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

            for (int i = 7; i <= 19; i++)
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
            dt2.Columns.AddRange(new DataColumn[2] { new DataColumn("RTimes"), new DataColumn("TStatus") });

            for (int i = 0; i < AvailableTimes.Count; i++)
            {
                string[] arr = new string[2];

                if (i < AvailableTimes.Count - 1)
                {
                    string tempTimes = AvailableTimes[i].Substring(0, (AvailableTimes[i].Length) - 3) + " to " + AvailableTimes[i + 1].Substring(0, (AvailableTimes[i].Length) - 3);

                    arr[0] = tempTimes;
                    arr[1] = TimeStatus[i].ToString();
                    dt2.Rows.Add(arr);
                }
            }
            return dt2;

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

            if (hr24 == 12 || hr24 == 11 || hr24 == 10)
            {
                rv = "" + hr24 + ":00:00";
            }
            else
            {
                rv = "0" + hr24 + ":00:00";

            }
            return rv;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (Calendar1.Visible)
            {
                Calendar1.Visible = false;
            }
            else
            {
                Calendar1.Visible = true;
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            string sdate = Calendar1.SelectedDate.ToString("yyyy-MM-dd");

            lblDate.Text = sdate;

            lblTable1.Text = "Table for 2";
            lblDate1.Text = sdate;

            lblTable2.Text = "Table for 4";
            lblDate2.Text = sdate;

            lblTable3.Text = "Table for 6";
            lblDate3.Text = sdate;

            lblTable4.Text = "Table for 10";
            lblDate4.Text = sdate;

            lblTable5.Text = "Table for 12";
            lblDate5.Text = sdate;

            rptAvailableTimes1.DataSource = DisplayAvailableTimeForChosenTable("Table for 2", sdate);
            rptAvailableTimes1.DataBind();

            rptAvailableTimes2.DataSource = DisplayAvailableTimeForChosenTable("Table for 4", sdate);
            rptAvailableTimes2.DataBind();

            rptAvailableTimes3.DataSource = DisplayAvailableTimeForChosenTable("Table for 6", sdate);
            rptAvailableTimes3.DataBind();

            rptAvailableTimes4.DataSource = DisplayAvailableTimeForChosenTable("Table for 10", sdate);
            rptAvailableTimes4.DataBind();

            rptAvailableTimes5.DataSource = DisplayAvailableTimeForChosenTable("Table for 12", sdate);
            rptAvailableTimes5.DataBind();

            Calendar1.Visible = false;

        }

        protected void rptAvailableTimes1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;
                if (row["TStatus"].ToString().Equals("Reserved"))
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

        protected void rptAvailableTimes2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;
                if (row["TStatus"].ToString().Equals("Reserved"))
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

        protected void rptAvailableTimes3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;
                if (row["TStatus"].ToString().Equals("Reserved"))
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

        protected void rptAvailableTimes4_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;
                if (row["TStatus"].ToString().Equals("Reserved"))
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

        protected void rptAvailableTimes5_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;
                if (row["TStatus"].ToString().Equals("Reserved"))
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
    }
}