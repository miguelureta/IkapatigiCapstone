using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkapatigiCapstone.Models
{
    public partial class User
    {
        //Orginally did not have this constructor
        public User()
        {
            AddRequestDiagnostics = new HashSet<AddRequestDiagnostic>();
            Forums = new HashSet<Forum>();
            HowTos = new HashSet<HowTo>();
            PlantDiseases = new HashSet<PlantDisease>();
            PostReplies = new HashSet<PostReply>();
            Posts = new HashSet<Post>();
            Tags = new HashSet<Tag>();
        }

        public int UserId { get; set; }
        public string? Username { get; set; }
        public byte[] PasswordHash { get; set; } = null!;
        public byte[]? PasswordSalt { get; set; }
        public string? VerificationToken { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DisplayName { get; set; }
        public string? Gender { get; set; }
        [Display(Name = "User Email")]
        public string Email { get; set; }
        public int? AccountNumber { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? RemainingSubscriptionDays { get; set; }
        public bool? CanceledSubscription { get; set; }
        public int? RoleId { get; set; }
        public int? SubscriptionId { get; set; }
        public int? StatusId { get; set; }

        //Below used to be public virtual Role UserNavigation { get; set; } = null!;
        public virtual Role? Role { get; set; }
        public virtual ICollection<AddRequestDiagnostic> AddRequestDiagnostics { get; set; }
        public virtual ICollection<Forum> Forums { get; set; }
        public virtual ICollection<HowTo> HowTos { get; set; }
        public virtual ICollection<PlantDisease> PlantDiseases { get; set; }
        public virtual ICollection<PostReply> PostReplies { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
