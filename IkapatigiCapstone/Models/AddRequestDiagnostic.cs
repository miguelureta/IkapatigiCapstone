using System;
using System.Collections.Generic;

namespace IkapatigiCapstone.Models
{
    public partial class AddRequestDiagnostic
    {
        public int AddRequestDiagnosticId { get; set; }
        public string? ImageName { get; set; }
        public string? TagName { get; set; }
        public string? CureName { get; set; }
        public string? DiseaseName { get; set; }
        public decimal? Srp { get; set; }
        public string? Remarks { get; set; }
        public int? ApprovedUserId { get; set; }
        public int? RequestedUserId { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateApproved { get; set; }
    }
}
