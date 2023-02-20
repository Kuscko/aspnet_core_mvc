namespace NFLTeams.Models
{
    public class TeamViewModel
    {
        public Team Team { get; set; }
        public string ActiveConf { get; set; } = "all";
        public string ActiveDiv { get; set; } = "all";
    }
}
