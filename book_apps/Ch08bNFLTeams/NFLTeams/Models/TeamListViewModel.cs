using System.Collections.Generic;

namespace NFLTeams.Models
{
    public class TeamListViewModel : TeamViewModel
    {
        public List<Team> Teams { get; set; }

        // use full properties for Conferences and Divisions 
        // so can add 'All' item at beginning
        private List<Conference> conferences;
        public List<Conference> Conferences {
            get => conferences; 
            set {
                conferences = value;
                conferences.Insert(0, 
                    new Conference { ConferenceID = "all", Name = "All" });
            }
        }

        private List<Division> divisions;
        public List<Division> Divisions {
            get => divisions; 
            set {
                divisions = value;
                divisions.Insert(0,
                    new Division { DivisionID = "all", Name = "All" });
            }
        }

        // methods to help view determine active link
        public string CheckActiveConf(string c) => 
            c.ToLower() == ActiveConf.ToLower() ? "active" : "";
          
        public string CheckActiveDiv(string d) => 
            d.ToLower() == ActiveDiv.ToLower() ? "active" : "";
    }
}