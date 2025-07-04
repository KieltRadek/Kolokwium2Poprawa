namespace Kolokwium2.DTOs
{
    public class RacerParticipationsDto
    {
        public int RacerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public List<ParticipationDto> Participations { get; set; } = new List<ParticipationDto>();
    }

    public class ParticipationDto
    {
        public RaceDto Race { get; set; } = null!;
        public TrackDto Track { get; set; } = null!;
        public int Laps { get; set; }
        public int FinishTimeInSeconds { get; set; }
        public int Position { get; set; }
    }

    public class RaceDto
    {
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime Date { get; set; }
    }

    public class TrackDto
    {
        public string Name { get; set; } = null!;
        public double LengthInKm { get; set; }
    }
}