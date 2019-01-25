namespace MyFootballRestApi.Models
{
    public class Tournament : EntityBase<Tournament>
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public TournamentType TournamentType { get; set; }
        public bool IsGenerated { get; set; }
    }

    public enum TournamentType
    {
        League,
        Cup
    }
}
