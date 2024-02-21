using System;
using System.Collections.Generic;

namespace CarsMVC.Models
{
    public partial class Car
    {
        public Car()
        {
            Ownerships = new HashSet<Ownership>();
        }

        public Guid CarId { get; set; }
        public string? Brand { get; set; }
        public string? Type { get; set; }
        public string? RegistrationNumber { get; set; }
        public DateTime? ProductionDate { get; set; }

        public virtual ICollection<Ownership> Ownerships { get; set; }
    }
}
