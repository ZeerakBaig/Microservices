using Microsoft.AspNetCore.Mvc;
using Purchase.Data;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Purchase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private PurchaseDBDatabaseContext db;
        public PurchaseController(PurchaseDBDatabaseContext db)
        {
            this.db = db;
        }
        // Adding a new Purchase for a user
        [HttpPut]
        public Purchase.Model.Purchase addPurchase(Purchase.Model.Purchase purchase)
        {
            var newPurchase = new Purchase.Model.Purchase
            {
                Id = purchase.Id,
                VehicleId = purchase.VehicleId,
                Rating = purchase.Rating,
                DeliveryDate = purchase.DeliveryDate
            };
            db.Add(newPurchase);
            db.SaveChanges();
            return new Purchase.Model.Purchase
            {
                BridgeId = newPurchase.BridgeId,
                Id = newPurchase.Id,
                VehicleId = newPurchase.VehicleId,
                Rating = newPurchase.Rating,
                DeliveryDate = newPurchase.DeliveryDate
            };

        }

        //function to get All the purchases
        [HttpGet]
        public List<Purchase.Model.Purchase> getAllPurchases()
        {
            List<Purchase.Model.Purchase> lstPurchases = new List<Purchase.Model.Purchase>();
            var purchase = (from p in db.Purchase
                            select p);
            if (purchase != null)
            {
                foreach (Purchase.Model.Purchase p in purchase)
                {
                    lstPurchases.Add(p);
                }
            }
            else
            {
                return null;
            }

            return lstPurchases;

        }

        //function to cancel a users purchase
        [HttpDelete]
        public Purchase.Model.Purchase cancelPurchase(int BridgeId)
        {
            var purchase = (from p in db.Purchase
                            where p.BridgeId == BridgeId
                            select p).FirstOrDefault();
            if (purchase != null)
            {
                db.Remove(db.Purchase.Find(BridgeId));
                db.SaveChanges();
                return purchase;
            }
            else
            {
                return new Purchase.Model.Purchase { BridgeId = -2 };
            }
        }

        //function to get All the purchases made by a customer
        [HttpGet("{Id}/Customer")]
        public List<Purchase.Model.Purchase> getAllCustomerPurchases(int Id)
        {
            List<Purchase.Model.Purchase> lstCustomerPurchases = new List<Purchase.Model.Purchase>();
            var purchases = (from p in db.Purchase
                             where p.Id.Equals(Id)
                             select p);
            if (purchases != null)
            {
                foreach (Purchase.Model.Purchase p in purchases)
                {
                    lstCustomerPurchases.Add(p);
                }
            }
            else
            {
                return null;
            }

            return lstCustomerPurchases;
        }

    }
}
