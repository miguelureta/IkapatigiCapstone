using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IkapatigiCapstone.Models
{
    public partial class Cure
    {
        public Cure()
        {
            Diagnostics = new HashSet<Diagnostic>();
        }
        
        public int CureId { get; set; }
        //Removed a null! after CureName
        public string CureName { get; set; }
        public decimal Srp { get; set; }
        public int? UserId { get; set; }

        public virtual ICollection<Diagnostic> Diagnostics { get; set; }
    }
}
