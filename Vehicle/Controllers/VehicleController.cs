using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Vehicle.Data;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vehicle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private VehicleDBDatabaseContext db;
        public VehicleController(VehicleDBDatabaseContext db)
        {
            this.db = db;
        }

        // method to add a new vehicle to the database
        [HttpPost]
        public Vehicle.Model.Vehicle addVehicle(Vehicle.Model.Vehicle vehicle)
        {
            var newVehicle = new Vehicle.Model.Vehicle
            {
                VehicleId = vehicle.VehicleId,
                VehicleBrand = vehicle.VehicleBrand,
                VehicleModel = vehicle.VehicleModel,
                VehicleType = vehicle.VehicleType,
                VehicleImage = vehicle.VehicleImage,
                VehiclePrice = vehicle.VehiclePrice,
                VehicleDescription = vehicle.VehicleDescription,
                VehicleEngine = vehicle.VehicleEngine,
                VehicleColour = vehicle.VehicleColour,
                VehicleQuantity = vehicle.VehicleQuantity,
            };
            db.Vehicle.Add(newVehicle);
            try
            {
                db.SaveChanges();
                return newVehicle;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return new Vehicle.Model.Vehicle { VehicleId = 0 };
            }
        }

        //function to get all the vehicles in the vehicle table
        [HttpGet]
        public List<Vehicle.Model.Vehicle> getAllvehicles()
        {
            List<Vehicle.Model.Vehicle> lstVehicles = new List<Vehicle.Model.Vehicle>();
            var vehicles = (from v in db.Vehicle
                            select v);
            if (vehicles != null)
            {
                foreach (Vehicle.Model.Vehicle v in vehicles)
                {
                    lstVehicles.Add(v);
                }
            }
            else
            {
                return null;
            }
            return lstVehicles;
        }


        //function to get a specific vehicle by its ID
        [HttpGet("{VehicleId}")]
        public Vehicle.Model.Vehicle getVehicleByID(int VehicleId)
        {
            var vehicle = (from v in db.Vehicle
                           where v.VehicleId == VehicleId
                           select v).FirstOrDefault();
            if (vehicle != null)
            {
                return vehicle;
            }
            else
            {
                return null;
            }
        }
        //method to update a vehicles details
        [HttpPut]
        public Vehicle.Model.Vehicle updateVehicleDetails(Vehicle.Model.Vehicle updateVehicle)
        {
            Vehicle.Model.Vehicle existingVehicle = (from v in db.Vehicle
                                       where v.VehicleId.Equals(updateVehicle.VehicleId)
                                       select v).FirstOrDefault();
            if (existingVehicle != null)
            {
                existingVehicle.VehicleBrand = updateVehicle.VehicleBrand;
                existingVehicle.VehicleModel = updateVehicle.VehicleModel;
                existingVehicle.VehicleType = updateVehicle.VehicleType;
                existingVehicle.VehicleImage = updateVehicle.VehicleImage;
                existingVehicle.VehiclePrice = updateVehicle.VehiclePrice;
                existingVehicle.VehicleDescription = updateVehicle.VehicleDescription;
                existingVehicle.VehicleEngine = updateVehicle.VehicleEngine;
                existingVehicle.VehicleColour = updateVehicle.VehicleColour;
                existingVehicle.VehicleQuantity = updateVehicle.VehicleQuantity;

                try
                {
                    db.SaveChanges();
                    return new Vehicle.Model.Vehicle
                    {
                        VehicleId = updateVehicle.VehicleId,
                        VehicleBrand = updateVehicle.VehicleBrand,
                        VehicleModel = updateVehicle.VehicleModel,
                        VehicleType = updateVehicle.VehicleType,
                        VehicleImage = updateVehicle.VehicleImage,
                        VehiclePrice = updateVehicle.VehiclePrice,
                        VehicleDescription = updateVehicle.VehicleDescription,
                        VehicleEngine = updateVehicle.VehicleEngine,
                        VehicleColour = updateVehicle.VehicleColour,
                        VehicleQuantity = updateVehicle.VehicleQuantity

                    };
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return new Vehicle.Model.Vehicle { VehicleId = -1 };
                }
            }
            else
            {
                return new Vehicle.Model.Vehicle { VehicleId = -2 };
            }

        }

        //function to delete a vehicle from the database given a vehicle ID
        [HttpDelete("{VehicleId}")]
        public Vehicle.Model.Vehicle deleteVehicle(int VehicleId)
        {
            var vehicleToDelete = (from v in db.Vehicle
                                   where v.VehicleId.Equals(VehicleId)
                                   select v).FirstOrDefault();
            if (vehicleToDelete != null)
            {
                db.Vehicle.Remove(db.Vehicle.Find(VehicleId));
                db.SaveChanges();
                return vehicleToDelete;
            }
            else
            {
                //no such vehicle exist that can be deleted
                return new Vehicle.Model.Vehicle { VehicleId = 0 };
            }
        }

    }
}
