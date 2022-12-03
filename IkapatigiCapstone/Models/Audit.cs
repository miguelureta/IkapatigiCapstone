namespace IkapatigiCapstone.Models
{
    public class Audit
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Reason { get; set; }
        public int? RoleId { get; set; }
    }
}
