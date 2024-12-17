namespace backend_solar.Models {
    public class User {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public List<Place> UserPlaces { get; set; } = new List<Place>();
    }
}
