using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login_MVC_.Models;
using System.Data.SqlClient;

namespace Login_MVC_.Controllers
{
    public class UserController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        // GET: User
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        void connectionString()
        {
            con.ConnectionString = "Data Source=VINA; database=coffeeshop; Integrated Security=SSPI;";
        }

        [HttpPost]
        public ActionResult Verify(User us)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from [dbo].[user] where username='"+us.username+"' and password='"+us.password+"';";
            dr = com.ExecuteReader();
            if(dr.Read())
            {
                con.Close();
                return View("Create");
            }
            else
            {
                con.Close();
                return View("Error");
            }
            
        }
    }
}