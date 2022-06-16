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
    public partial class Item : System.Web.UI.Page
    {
        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("Menu.aspx");
            }

            else
            {
                id = Convert.ToInt32(Request.QueryString["id"].ToString());
                DataTable dt = getData("select * from tblMeals where MealId = " + id);

                Repeater1.DataSource = dt;
                Repeater1.DataBind();
 
            }

        }        

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("UserLogin.aspx");
            }

            else
            {
                string cookies = "";

                id = Convert.ToInt32(Request.QueryString["id"].ToString());
                DataTable dt = getData("select * from tblMeals where MealId = " + id);

                foreach (DataRow dr in dt.Rows)
                {
                    cookies = dr["Name"].ToString() + "-" +
                              dr["Price"].ToString() + "-" +
                              dr["Description"].ToString() + "-" +
                              dr["image"].ToString();

                }

                if (Request.Cookies["Order"] != null)
                {
                    string temp = Convert.ToString(Request.Cookies["Order"].Value);
                    if (temp.Contains(cookies))
                    {
                        string temp2 = temp.Substring(temp.IndexOf(cookies), cookies.Length + 2);
                        string qtyString = temp2.Substring(cookies.Length + 1);

                        int qtyInt = Convert.ToInt32(qtyString);
                        string cookiesNew;
                        string cookiesOld = cookies + "-" + qtyInt;

                        qtyInt = qtyInt + 1;
                        cookiesNew = cookies + "-" + qtyInt;

                        temp = temp.Replace(cookiesOld, cookiesNew);

                        Response.Cookies["Order"].Value = temp;
                        Response.Cookies["Order"].Expires = DateTime.Now.AddDays(1);
                    }
                    else
                    {
                        cookies = cookies + "-" + 1;
                        Response.Cookies["Order"].Value = Request.Cookies["Order"].Value + "|" + cookies;
                        Response.Cookies["Order"].Expires = DateTime.Now.AddDays(1);
                    }
                }
                else
                {
                    cookies = cookies + "-" + 1;
                    Response.Cookies["Order"].Value = cookies;
                    Response.Cookies["Order"].Expires = DateTime.Now.AddDays(1);
                }
            }
        }

        private DataTable getData(String query)
        {
            SqlConnection con = new SqlConnection(CS);
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
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