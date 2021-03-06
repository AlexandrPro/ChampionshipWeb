//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChampionshipWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public team()
        {
            this.games = new HashSet<game>();
            this.games1 = new HashSet<game>();
            this.players = new HashSet<player>();
            this.teams_in_stage = new HashSet<teams_in_stage>();
        }
    
        public int id { get; set; }
        public string short_name { get; set; }
        public string name { get; set; }
        public Nullable<int> coach_id { get; set; }
        public Nullable<int> citys_id { get; set; }
        public string photo_path { get; set; }
    
        public virtual city city { get; set; }
        public virtual coach coach { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<game> games { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<game> games1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<player> players { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<teams_in_stage> teams_in_stage { get; set; }
    }
}
