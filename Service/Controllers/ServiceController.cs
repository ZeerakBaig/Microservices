using Microsoft.AspNetCore.Mvc;
using Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        // GET: api/<ServiceController>
        public ServiceDBDatabaseContext db;
        public ServiceController(ServiceDBDatabaseContext db)
        {
            this.db = db;
        }

        [HttpGet]
        //function to get all the service requests for all the customers
        public List<Service.Model.Service> getAllServiceRequests()
        {
            List<Service.Model.Service> lstServices = new List<Service.Model.Service>();
            var requests = (from s in db.Service
                            select s);
            if (requests != null)
            {
                foreach (Service.Model.Service r in requests)
                {
                    lstServices.Add(r);
                }
            }
            else
            {
                return null;
            }

            return lstServices;
        }

        //function to post a service request by a user
        [HttpPost]
        public Service.Model.Service requestService(Service.Model.Service request)
        {
            var newRequest = new Service.Model.Service
            {
                MechanicId = request.MechanicId,
                Id = request.Id,
                RequestDate = request.RequestDate,
                RequestStatus = request.RequestStatus,
                CustomerReview = request.CustomerReview,
                MechanicRating = request.MechanicRating
            };
            db.Service.Add(newRequest);
            try
            {
                db.SaveChanges();
                return new Service.Model.Service
                {
                    ServiceId = newRequest.ServiceId,
                    MechanicId = newRequest.MechanicId,
                    Id = newRequest.Id,
                    RequestDate = newRequest.RequestDate,
                    RequestStatus = newRequest.RequestStatus,
                    CustomerReview = newRequest.CustomerReview,
                    MechanicRating = newRequest.MechanicRating
                };
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return new Service.Model.Service { MechanicId = -1, Id = -1 };
            }

        }

        //function to get a list of service requests by a customer
        [HttpGet("{Id}/User")]
        public List<Service.Model.Service> getUserRequests(int Id)
        {
            List<Service.Model.Service> lstRequests = new List<Service.Model.Service>();
            var req = (from r in db.Service
                       where r.Id == Id
                       select r);
            if (req != null)
            {
                foreach (Service.Model.Service sr in req)
                {
                    lstRequests.Add(sr);
                }
            }
            else
            {
                return null;
            }

            return lstRequests;
        }

        //function to get a specific users review
        [HttpGet("User/{Id}/review")]
        public List<String> getUserReviews(int Id)
        {
            List<String> lstReviews = new List<String>();
            var req = (from r in db.Service
                       where r.Id.Equals(Id)
                       select r);
            if (req != null)
            {
                foreach (Service.Model.Service sr in req)
                {
                    lstReviews.Add(sr.CustomerReview);
                }
            }
            else
            {
                return null;
            }

            return lstReviews;
        }

        //function to delete a service Request
        [HttpDelete]
        public Service.Model.Service deleteServiceRequest(int ServiceId)
        {
            var req = (from r in db.Service
                       where r.ServiceId.Equals(ServiceId)
                       select r).FirstOrDefault();
            if (req != null)
            {
                db.Remove(db.Service.Find(ServiceId));
                db.SaveChanges();
                return req;  // return the request that was deleted
            }
            else
            {
                return new Service.Model.Service { Id = -1 }; // no such request exists
            }
        }


    }
}
