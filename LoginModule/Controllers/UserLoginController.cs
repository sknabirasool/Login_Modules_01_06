using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginModule.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security;


namespace LoginModule.Controllers
{

    public class UserLoginController : Controller
    {
        public string status;
        public string email;
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Index(Register e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=.;Integrated Security=true;Initial Catalog=Registration_db"))
            {
                string SqlQuery = "select email,password from tbl_users where email=@email and password=@password";
                con.Open();
                SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
                cmd.Parameters.AddWithValue("@email", e.email);
                cmd.Parameters.AddWithValue("@password", e.password);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    email = e.email.ToString();
                    return RedirectToAction("Welcome");
                }
                else
                {
                    ViewData["Message"] = "User Login Details Failed!!";
                }
                if (e.email.ToString() != null)
                {
                    //Session["Email"] = e.Email.ToString();
                    status = "1";
                }
                else
                {
                    status = "3";
                }

                con.Close();
                return View();
                //return new JsonResult { Data = new { status = status } };  
            }
        }

            [HttpGet]
            public ActionResult Welcome(Register e)
            {
                Register user = new Register();
                DataSet ds = new DataSet();

                using (SqlConnection con = new SqlConnection("Data Source=.;Integrated Security=true;Initial Catalog=Registration_db"))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar, 30).Value = email.ToString();
                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(ds);
                        List<Register> userlist = new List<Register>();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Register uobj = new Register();
                            uobj.user_name = ds.Tables[0].Rows[i]["user_name"].ToString();
                            uobj.email = ds.Tables[0].Rows[i]["email"].ToString();
                            uobj.password = ds.Tables[0].Rows[i]["password"].ToString();

                            userlist.Add(uobj);

                        }
                        user.registerinfo = userlist;
                    }
                    con.Close();

                }
                return View(user);
            }
        
            public ActionResult Logout()
            {
                //FormsAuthentication.SignOut();

                return RedirectToAction("Index", "UserLogin");
            }

        }
    }

