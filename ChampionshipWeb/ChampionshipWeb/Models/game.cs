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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    public partial class game
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public game()
        {
            this.statistics = new HashSet<statistic>();
            this.judges = new HashSet<judge>();
        }
    
        public int id { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime date { get; set; }
        public Nullable<int> team1_id { get; set; }
        public Nullable<int> team2_id { get; set; }
        public Nullable<int> team1_score { get; set; }
        public Nullable<int> team2_score { get; set; }
        public string place { get; set; }
        public int game_state_id { get; set; }
        public int stage_id { get; set; }
    
        public virtual game_state game_state { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<statistic> statistics { get; set; }
        public virtual stage stage { get; set; }
        public virtual team team { get; set; }
        public virtual team team1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<judge> judges { get; set; }
    }
}
