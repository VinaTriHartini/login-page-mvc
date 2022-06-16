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
        string big;
        string part;
        string[] strRow = new string[5];

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] {new DataColumn("Name"), new DataColumn("Price"), new DataColumn("Description"), new DataColumn("image"), new DataColumn("quantity")});

            if(Request.Cookies["Order"] != null)
            {
                big = Convert.ToString(Request.Cookies["Order"].Value);

                string[] Arr = big.Split('|');

                for (int i = 0; i < Arr.Length; i++)
                {
                    part = Convert.ToString(Arr[i].ToString());
                    string[] Arr1 = part.Split('-');

                    for (int j = 0; j < Arr1.Length; j++)
                    {
                        strRow[j] = Arr1[j].ToString();
                    }
                    dt.Rows.Add(strRow);
                }
                rptOrder.DataSource = dt;
                rptOrder.DataBind();
            }
        }
    }
}