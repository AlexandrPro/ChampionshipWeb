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
    
    public partial class statistic
    {
        public int games_id { get; set; }
        public int players_id { get; set; }
        public int free_throws_attempts { get; set; }
        public int free_throws_made { get; set; }
        public int two_point_throws_attempts { get; set; }
        public int two_point_throws_made { get; set; }
        public int three_point_throws_attempts { get; set; }
        public int three_point_throws_made { get; set; }
        public Nullable<int> total_score { get; set; }
        public int offensive_rebounds { get; set; }
        public int deffensive_rebounds { get; set; }
        public int assists { get; set; }
        public int steals { get; set; }
        public int turnovers { get; set; }
        public int blocked_shots { get; set; }
        public int commited_fouls { get; set; }
        public int recieved_fouls { get; set; }
    
        public virtual game game { get; set; }
        public virtual player player { get; set; }
    }
}
