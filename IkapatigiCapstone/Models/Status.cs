using System;
using System.Collections.Generic;

namespace IkapatigiCapstone.Models
{
    public partial class Status
    {
        public Status()
        {
            Diagnostics = new HashSet<Diagnostic>();
        }

        public int StatusId { get; set; }
        public string StatusType { get; set; } = null!;

        public virtual ICollection<Diagnostic> Diagnostics { get; set; }
    }
}
