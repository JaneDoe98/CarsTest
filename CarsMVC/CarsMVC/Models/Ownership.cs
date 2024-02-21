using System;
using System.Collections.Generic;

namespace CarsMVC.Models
{
    public partial class Ownership
    {
        public Guid OwnershipId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid CarId { get; set; }

        public virtual Car? Car { get; set; }
        public virtual Owner? Owner { get; set; }
    }
}
