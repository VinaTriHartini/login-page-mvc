using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Reg_User.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace MVC_Reg_User.Controllers
{
    public class NewRegController : Controller
    {
        // GET: NewReg
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserClass uc, HttpPostedFileBase file)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "insert into [dbo].[user] (username, role, password) values (@username, @role, @password)";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            sqlcomm.Parameters.AddWithValue("@username", uc.username);
            sqlcomm.Parameters.AddWithValue("@role", uc.role);
            sqlcomm.Parameters.AddWithValue("@password", uc.username);
            sqlcomm.ExecuteNonQuery();
            sqlconn.Close();
            ViewData["Message"] = "User record " + uc.username + " Is Saved Succesfully !";
            return View();
        }
    }
}