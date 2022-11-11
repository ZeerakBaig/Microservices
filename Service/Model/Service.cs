using System;
using System.ComponentModel.DataAnnotations;

namespace Service.Model
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public int MechanicId { get; set; }
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestStatus { get; set; }
        public string CustomerReview { get; set; }
        public Nullable<int> MechanicRating { get; set; }
    }
}
