using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceDbFirst.Models
{
    public class CustomerManager
    {
        ECommerceEntities db = new ECommerceEntities();

        public IList<Customer> GetCustomersInCity(string city)
        {
            var result = db.Customers.Where(c => c.City == city).ToList();
            return result;
        }
    }
}