using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using User.Data;
using System.Linq;
using System;
using User.Model;
using RestSharp;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Web.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserDatabaseContext db;
        public UserController(UserDatabaseContext db)
        {
            this.db = db;
        }


        //Function to get all the users of the system
        [HttpGet]
        public List<User.Model.User> Get()
        {
            List<User.Model.User> lstAllUsers = new List<User.Model.User>();
            var usr = (from u in db.User
                       select u);
            if (usr != null)
            {
                foreach (User.Model.User u in usr)
                {
                    lstAllUsers.Add(u);
                }
            }
            else
            {
                return null;
            }

            return lstAllUsers;
        }


        // GET api/<UserController>/5
        //Function to get a user by ID
        [HttpGet("{Id}")]
        public User.Model.User Get(int Id)
        {

            var usr = (from b in db.User
                       where b.Id.Equals(Id)
                       select b).FirstOrDefault();


            return usr;
        }


        [HttpPost]
        //Function to register a user
        public User.Model.User register(User.Model.User user)
        {
            //check if the user already exists
            var usr = (from u in db.User
                       where u.UserEmail == user.UserEmail
                       select u).FirstOrDefault();

            if (usr == null)
            {
                String spice = Guid.NewGuid().ToString();

                var newUser = new User.Model.User
                {
                    UserName = user.UserName,
                    UserSurname = user.UserSurname,
                    UserEmail = user.UserEmail,
                    UserPassword = Protection.hash(spice + user.UserPassword),
                    UserType = user.UserType,
                    Status = user.Status,
                    SpiceCode = spice
                };
                db.User.Add(newUser);
                try
                {
                    db.SaveChanges();

                    return new User.Model.User
                    {
                        Id = newUser.Id,
                        UserName = newUser.UserName,
                        UserSurname = newUser.UserSurname,
                        UserEmail = newUser.UserEmail,
                        UserType = newUser.UserType,
                        Status = newUser.Status,

                    };
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return new User.Model.User { Id = -1 }; ;
                }
            }
            else
            {
                return new User.Model.User { Id = 0 };
            }
        }

        [HttpPost("Login")]
        public User.Model.User Login(User.Model.User usr)
        {
            var user = (from u in db.User
                        where u.UserEmail.Equals(usr.UserEmail)
                        select u).FirstOrDefault();
            if (user == null)
            {
                return new User.Model.User { Id = -1 };
            }
            else
            {
                String password = Protection.hash(user.SpiceCode + usr.UserPassword);
                if (user.UserPassword.Equals(password))
                {
                    return new User.Model.User
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        UserSurname = user.UserSurname,
                        UserEmail = user.UserEmail,
                        UserType = user.UserType,
                        Status = user.Status,
                    };
                }
                else
                {
                    return new User.Model.User { Id = 0, UserPassword = "Incorrect Password" };
                }
            }

        }

        /* [HttpPut]
         public User.Model.User updateUserAccountDetails(UpdateUser usr)
         {
             //find the user with the same ID
             User.Model.User user = (from u in db.User
                          where u.Id.Equals(usr.Id)
                          select u).FirstOrDefault();

             if (user == null)
             {
                 //no such user exists
                 return new User.Model.User { Id = 0 };
             }
             else
             {
                 //user does exist
                 user.UserName = usr.newName;
                 user.UserSurname = usr.newSurname;
                 user.Status = usr.newStatus;

                 try
                 {
                     db.SaveChanges();
                     return new User.Model.User
                     {
                         Id = user.Id,
                         UserName = user.UserName,
                         UserSurname = user.UserSurname,
                         UserEmail = user.UserEmail,
                         UserType = user.UserType,
                         Status = user.Status
                     };
                 }
                 catch (Exception e)
                 {
                     e.GetBaseException();
                     return new User.Model.User { Id = -1 };
                 }

             }

         }*/

        //method used to delete a user from the database
        [HttpDelete("{Id}")]
        public User.Model.User deleteUser(int Id)
        {
            var usr = (from u in db.User
                       where u.Id.Equals(Id)
                       select u).FirstOrDefault();
            if (usr != null)
            {
                //check if the user is a mechanic

                db.User.Remove(db.User.Find(usr.Id));
                db.SaveChanges();
                return usr;
            }
            else
            {
                return new User.Model.User { Id = 0 }; // no such user exists
            }
        }

        //registering a new mechanic
        [HttpPost("Mechanic")]
        public  Mechanic RegisterMechanic(MechanicRegistration mech)
        {
            User.Model.User usr = new User.Model.User
            {
                UserName = mech.UserName,
                UserSurname = mech.UserSurname,
                UserEmail = mech.UserEmail,
                UserPassword = mech.UserPassword,
                UserType = "Mechanic",
                Status = "Active",
                SpiceCode = null
            };
            User.Model.User newUser = register(usr);
            if (newUser.Id > 0)
            {
                Mechanic mechanic = new Mechanic
                {
                    MechanicId = newUser.Id,
                    MechanicRegNumber = mech.MechanicRegNumber,
                    MechanicSpeciality = mech.MechanicSpeciality,
                    YearsOfExperience = mech.YearsOfExperience,
                    Address = mech.Address
                };

                RestClient client = new RestClient("https://localhost:44359/api/");
                var request = new RestRequest("Mechanic").AddJsonBody(mechanic);
                var response = client.ExecutePostAsync <Mechanic>(request);
                return mechanic;


              /*  JObject jObjectBody = new JObject();
                jObjectBody.Add("mechanicId", mechanic.MechanicId);
                jObjectBody.Add("mechanicRegNumber", mechanic.MechanicRegNumber);
                jObjectBody.Add("mechanicSpeciality", mechanic.MechanicSpeciality);
                jObjectBody.Add("yearsOfExperience", mechanic.YearsOfExperience);
                jObjectBody.Add("address", mechanic.Address);

                RestRequest restRequest = new RestRequest("/Mechanic",Method.Post);
                restRequest.AddParameter("application/json; charset=utf-8", jObjectBody, ParameterType.RequestBody);
                client.ExecuteAsync(restRequest);*/
               
                return mechanic;
                //send the mechanic to the mechanic controller
                /*  db.Mechanic.Add(mechanic);
                  try
                  {
                      db.SaveChanges();
                      return mechanic;
                  }
                  catch (Exception ex)
                  {
                      ex.GetBaseException();
                      return new Mechanic { MechanicId = -4 };
                  }*/


            }
            else if (newUser.Id == 0)
            {
                return new Mechanic { MechanicId = 0 };
            }
            else
            {
                return new Mechanic { MechanicId = -1 };
            }

        }
    }
}
