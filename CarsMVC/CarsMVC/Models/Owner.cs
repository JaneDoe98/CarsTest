using System;
using System.Collections.Generic;

namespace CarsMVC.Models
{
    public partial class Owner
    {
        public Owner()
        {
            Ownerships = new HashSet<Ownership>();
        }

        public Guid OwnerId { get; set; }
        public string? Surname { get; set; }
        public string? Firstname { get; set; }
        public DateTime? BirthDate { get; set; }

        public virtual ICollection<Ownership> Ownerships { get; set; }
    }
}
