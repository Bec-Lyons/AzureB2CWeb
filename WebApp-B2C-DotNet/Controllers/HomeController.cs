using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp_OpenIDConnect_DotNet_B2C.Models;

namespace WebApp_OpenIDConnect_DotNet_B2C.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {

            Claim displayName = ClaimsPrincipal.Current.FindFirst(ClaimsPrincipal.Current.Identities.First().NameClaimType);
            ViewBag.DisplayName = displayName != null ? "Signed in as: " + displayName.Value : "Welcome. Sign in to Continue.";

            string display = displayName != null ? "" + displayName.Value : "Test";

            try
            {
                ConnectionStringSettings settings;
                settings = System.Configuration.ConfigurationManager.ConnectionStrings["RLDBConnectionString"];
                string connectionString = "Server=tcp:melbourneserver.database.windows.net,1433; Initial Catalog = RelyB2CDemoData; Persist Security Info = False; User ID = adminlogin; Password =P4ssw0rd!123; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
                SqlConnection myConnection = new SqlConnection(connectionString);
                myConnection.Open();

                string command = "select * from DemoTransactions where DisplayName like '" + display + "';";

                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(command,
                                                         myConnection);
                myReader = myCommand.ExecuteReader();

                List<DummyData> allFiles = new List<DummyData>();
                string color = "#ffffff";
                while (myReader.Read())
                {
                    if (myReader["orderName"].ToString().Equals("Pencil"))
                    {
                        color = "#9AD1D4";
                    }
                    else if (myReader["orderName"].ToString().Equals("Pen"))
                    {
                        color = "#80CED7";
                    }
                    else
                    {
                        color = "#007EA7";
                    }
                    allFiles.Add(new DummyData(myReader["orderName"].ToString(), myReader["orderDesc"].ToString(), myReader["date"].ToString(), color));

                }



                return View(allFiles);

            }
            catch (Exception ex)
            {
                ViewData["message"] = ex.Message;
                ViewData["trace"] = ex.StackTrace;

                ViewBag.Data = displayName;
                return View("Error");
            }
        }


        // You can use the Authorize decorator to execute a policy if the user is not already signed in the app.
        [Authorize]
        public ActionResult Claims()
        {
            //extract claims about the user
            Claim displayName = ClaimsPrincipal.Current.FindFirst(ClaimsPrincipal.Current.Identities.First().NameClaimType);
            ViewBag.DisplayName = displayName != null ? displayName.Value : string.Empty;
            return View();

        }

        public ActionResult Error(string message)
        {
            ViewBag.Message = message;

            return View("Error");
        }

        





        //DEMO LOGIC
        public async Task<ActionResult> OrderPencil()
        {
            Console.WriteLine("REACHED");

            string ordername = "Pencil";
            string orderdesc = "An instrument for writing or drawing, consisting of a thin stick of graphite or a similar substance enclosed in a long thin piece of wood or fixed in a cylindrical case.";


            Claim displayName = ClaimsPrincipal.Current.FindFirst(ClaimsPrincipal.Current.Identities.First().NameClaimType);

            string display = displayName != null ? "" + displayName.Value : "Test";

            
            string cmdString = "INSERT INTO DemoTransactions (DisplayName, orderName, orderDesc, date) VALUES ('" + display + "', '" + ordername + "', '" + orderdesc + "', '"+ DateTime.Now+"')";
            string connString = "Server=tcp:melbourneserver.database.windows.net,1433; Initial Catalog = RelyB2CDemoData; Persist Security Info = False; User ID = adminlogin; Password =P4ssw0rd!123; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = cmdString;

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                        return RedirectToAction("Index");
                    }
                    catch (SqlException ex)
                    {
                        ViewData["message"] = ex.Message;
                        ViewData["trace"] = ex.StackTrace;
                        ViewBag.Data = ex.Message;
                        return View("Error");
                    }
                }

            }
        }

        public async Task<ActionResult> OrderPen()
        {
            Console.WriteLine("REACHED");

            string ordername = "Pen";
            string orderdesc = "an instrument for writing or drawing with ink, typically consisting of a metal nib or ball, or a nylon tip, fitted into a metal or plastic holder.";

            Claim displayName = ClaimsPrincipal.Current.FindFirst(ClaimsPrincipal.Current.Identities.First().NameClaimType);

            string display = displayName != null ? "" + displayName.Value : "Test";


            string cmdString = "INSERT INTO DemoTransactions (DisplayName, orderName, orderDesc, date) VALUES ('" + display + "', '" + ordername + "', '" + orderdesc + "', '"+ DateTime.Now+"')";
            string connString = "Server=tcp:melbourneserver.database.windows.net,1433; Initial Catalog = RelyB2CDemoData; Persist Security Info = False; User ID = adminlogin; Password =P4ssw0rd!123; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = cmdString;

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                        return RedirectToAction("Index");
                    }
                    catch (SqlException ex)
                    {
                        ViewData["message"] = ex.Message;
                        ViewData["trace"] = ex.StackTrace;
                        ViewBag.Data = ex.Message;
                        return View("Error");
                    }
                }

            }
        }

        public async Task<ActionResult> OrderEraser()
        {
            Console.WriteLine("REACHED");

            string ordername = "Eraser";
            string orderdesc = "A piece of soft rubber or plastic used to rub out something written.";

            Claim displayName = ClaimsPrincipal.Current.FindFirst(ClaimsPrincipal.Current.Identities.First().NameClaimType);

            string display = displayName != null ? "" + displayName.Value : "Test";


            string cmdString = "INSERT INTO DemoTransactions (DisplayName, orderName, orderDesc, date) VALUES ('" + display + "', '" + ordername + "', '" + orderdesc + "', '" + DateTime.Now + "')";
            string connString = "Server=tcp:melbourneserver.database.windows.net,1433; Initial Catalog = RelyB2CDemoData; Persist Security Info = False; User ID = adminlogin; Password =P4ssw0rd!123; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";


            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = cmdString;

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                        return RedirectToAction("Index");
                    }
                    catch (SqlException ex)
                    {
                        ViewData["message"] = ex.Message;
                        ViewData["trace"] = ex.StackTrace;
                        ViewBag.Data = ex.Message;
                        return View("Error");
                    }
                }

            }
        }
    }
}