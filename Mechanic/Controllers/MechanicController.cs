using Mechanic.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Mechanic.Model;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mechanic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechanicController : ControllerBase
    {
        private MechanicDatabaseContext db;
        public MechanicController(MechanicDatabaseContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public List<Mechanic.Model.Mechanic> getAllMechanics()
        {
            List<Mechanic.Model.Mechanic> mechList = new List<Mechanic.Model.Mechanic>();

            var mechanic = (from m in db.Mechanic
                            select m);
            if (mechanic == null)
            {
                return null;
            }
            else
            {
                foreach (Mechanic.Model.Mechanic m in mechanic)
                {
                    mechList.Add(m);
                }

            }
            return mechList;
        }

        //getting mechanic by an ID
        [HttpGet("{Id}")]
        public Mechanic.Model.Mechanic getMechanicByID(int Id)
        {
            var mechanic = (from m in db.Mechanic
                            where m.MechanicId.Equals(Id)
                            select m).FirstOrDefault();
            if (mechanic == null)
            {
                return new Mechanic.Model.Mechanic { MechanicId = 0 }; // no mechanic exists
            }
            else
            {
                return mechanic;
            }
        }


        // function to register a mechanic
        [HttpPost]
        public Mechanic.Model.Mechanic registerMechanic(Mechanic.Model.Mechanic mechanic)
        {

            db.Mechanic.Add(mechanic);
            try
            {
                db.SaveChanges();
                return mechanic;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return new Mechanic.Model.Mechanic { MechanicId = -4 };
            }
        }


        //method to update mechanic details
        [HttpPut]
        public Mechanic.Model.Mechanic updateMechanicDetails(Mechanic.Model.Mechanic updateMechanic)
        {
            var existingMechanic = (from m in db.Mechanic
                                    where m.MechanicId.Equals(updateMechanic.MechanicId)
                                    select m).FirstOrDefault();
            if (existingMechanic == null)
            {
                //mechanic does not exist
                return new Mechanic.Model.Mechanic { MechanicId = 0 };
            }
            else
            {
                existingMechanic.MechanicRegNumber = updateMechanic.MechanicRegNumber;
                existingMechanic.MechanicSpeciality = updateMechanic.MechanicSpeciality;
                existingMechanic.YearsOfExperience = updateMechanic.YearsOfExperience;
                existingMechanic.Address = updateMechanic.Address;
                try
                {
                    db.SaveChanges();
                    return new Mechanic.Model.Mechanic
                    {
                        MechanicId = updateMechanic.MechanicId,
                        MechanicRegNumber = updateMechanic.MechanicRegNumber,
                        MechanicSpeciality = updateMechanic.MechanicSpeciality,
                        YearsOfExperience = updateMechanic.YearsOfExperience,
                        Address = updateMechanic.Address
                    };
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return new Mechanic.Model.Mechanic { MechanicId = -1 };
                }

            }


        }
    }
}
