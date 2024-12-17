namespace backend_solar.Models {
    public class Place {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<Sensor> Sensors { get; set; } = new List<Sensor>();
        public List<User> AllowedUsers { get; set; } = new List<User>();
    }
}
