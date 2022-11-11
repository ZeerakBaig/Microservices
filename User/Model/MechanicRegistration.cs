using System;

namespace User.Model
{
    public class MechanicRegistration
    {
        public String UserName { get; set; }
        public String UserSurname { get; set; }
        public String UserPassword { get; set; }
        public String UserEmail { get; set; }

        public String MechanicRegNumber { get; set; }
        public String MechanicSpeciality { get; set; }
        public int YearsOfExperience { get; set; }
        public String Address { get; set; }
    }
}
