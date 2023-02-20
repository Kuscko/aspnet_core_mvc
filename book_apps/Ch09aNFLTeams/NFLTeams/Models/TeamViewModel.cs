using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFLTeams.Models
{
    public class TeamViewModel
    {
        public Team Team { get; set; }
        public string ActiveConf { get; set; }
        public string ActiveDiv { get; set; }
    }
}
