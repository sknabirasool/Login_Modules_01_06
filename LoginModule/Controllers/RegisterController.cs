using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginModule.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace LoginModule.Controllers
{
    public class RegisterController : Controller
    {
        public string value = "";

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Register e)
        {
            if (Request.Method == "POST")
            {
                Register er = new Register();
                using (SqlConnection con = new SqlConnection("Data Source=.;Integrated Security=true;Initial Catalog=Registration_db"))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_User_reg", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@user_name", e.user_name);
                        cmd.Parameters.AddWithValue("@email", e.email);
                        cmd.Parameters.AddWithValue("@password", e.password);
                        cmd.Parameters.AddWithValue("@status", "Insert");
                        con.Open();
                        ViewData["result"] = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            return View();
        }

    }
}

