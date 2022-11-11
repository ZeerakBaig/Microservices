using System;
using System.ComponentModel.DataAnnotations;

namespace Purchase.Model
{
    public class Purchase
    {
        [Key]
        public int BridgeId { get; set; }
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int Rating { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
