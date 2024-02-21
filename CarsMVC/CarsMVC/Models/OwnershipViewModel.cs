namespace CarsMVC.Models
{
    public class OwnershipViewModel
    {
        public string? OwnerName { get; set; }
        public Guid OwnerId { get; set; }

        public List<Car> CarList { get; set; }
    }
}
