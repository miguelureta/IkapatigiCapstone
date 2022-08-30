using System;
using System.Collections.Generic;

namespace IkapatigiCapstone.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DisplayName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public int? AccountNumber { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? RemainingSubscriptionDays { get; set; }
        public bool? CanceledSubscription { get; set; }
        public int? RoleId { get; set; }
        public int? SubscriptionId { get; set; }
        public int? StatusId { get; set; }
    }
}
