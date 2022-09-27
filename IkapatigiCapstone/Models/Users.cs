using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IkapatigiCapstone.Models
{
    public partial class Users
    {
        [Key]
        public int UserId { get; set; }

        //[DisplayName("User Name")]
        //[Required]
        public string? Username { get; set; }
        //[Required]
        public string? Password { get; set; }
        public byte[]? ProfilePicture { get; set; }
        //[DisplayName("First Name")]
        public string? FirstName { get; set; }
        //[DisplayName("Last Name")]
        public string? LastName { get; set; }
        //[DisplayName("Display Name")]
        public string? DisplayName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        //[DisplayName("Account Number")]
        public int? AccountNumber { get; set; }
        //[DisplayName("Date Created")]
        public DateTime? DateCreated { get; set; }
        //[DisplayName("Date Updated")]
        public DateTime? DateUpdated { get; set; }
        //[DisplayName("Remaining Subscription Time")]
        public int? RemainingSubscriptionDays { get; set; }
        
        public bool? CanceledSubscription { get; set; }
        public int? RoleId { get; set; }
        public int? SubscriptionId { get; set; }
        public int? StatusId { get; set; }
    }
}
