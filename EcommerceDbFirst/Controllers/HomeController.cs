using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceDbFirst.Controllers
{
    public class HomeController : Controller
    {
        ECommerceEntities db = new ECommerceEntities();
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult CustomersCities()
        {
            var result = db.Database.SqlQuery<string>("EXEC GetAllCities").ToList();
            Logger.Info("User is getting a list of citites");
            return View(result);
        }
        public ActionResult CustomersNames()
        {
            var result = db.Database.SqlQuery<string>("EXEC GetAllCustomersNames").ToList();
            return View("CustomersCities", result);
        }
        public ActionResult CustomersInCity(string targetCity)
        {
            var result = db.Database.SqlQuery<string>
                ("EXEC CustomersInCity @city", new SqlParameter("city", targetCity)).ToList();
            return View("CustomersCities", result);
        }

        public string FirstCustomerInCity(string targetCity)
        {
            var result = db.Database.SqlQuery<string>
                ("EXEC FirstCustomerInCity @city", new SqlParameter("city", targetCity)).FirstOrDefault();
            return result;
        }
        public ActionResult GetUserInfo(int id)
        {
            try
            {
                var result = db.Database.SqlQuery<UserInfo>
                    ("EXEC GetUserInfo @id", new SqlParameter("id", id)).ToList();
                return View(result);
            }
            catch (Exception c)
            {
                Logger.Error("An Error Occured while getting the user {0} info" + c.Message, id);
                return View(new List<UserInfo>());
            }
        }
        public ActionResult GetProductsWithCategories()
        {
            var result = db.Database.SqlQuery<ProductWithCategory>(
                "Exec GetProductsWithCategories").ToList();
            return View(result);
        }
        public ActionResult GetProductDetails(int id)
        {
            var result = db.Database.SqlQuery<ProductDetails>
                ("Exec GetProductDetails @id", new SqlParameter("id", id)).FirstOrDefault();
            return View(result);
        }
    }
    public class ProductDetails
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string CategoryName { get; set; }
    }
    public class ProductWithCategory
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
    }
    public class UserInfo
    {
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}