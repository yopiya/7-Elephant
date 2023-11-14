using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _7_Elephant.Controllers
{
    public class OrdersController : Controller
    {
        private Entities db = new Entities();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList().Where(orders => orders.user_email == User.Identity.Name));
        }

        [HttpPost]
        public ActionResult AddOrder(FormCollection fc)
        {
            Phone product = new Phone();
            int product_id = int.Parse(fc["C_product_id"]);
            int quantity = int.Parse(fc["quantity"]);
            product = db.Phones.SingleOrDefault(m => m.C_product_id == product_id);
            
            Order my_order = new Order();
            my_order.user_email = User.Identity.Name;
            my_order.product_id = product_id;
            my_order.quantity = quantity;
            my_order.total = product.price * quantity;
            my_order.address = fc["address"];
            
            db.Orders.Add(my_order);
            db.SaveChanges();
            
            return View("Index", db.Orders.ToList());
        }
    }
}
