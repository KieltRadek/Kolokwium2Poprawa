using System.ComponentModel.DataAnnotations;

namespace Kolokwium2.DTOs
{
    public class AddRaceParticipationsRequestDto
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string RaceName { get; set; } = null!;
        
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string TrackName { get; set; } = null!;
        
        [Required]
        [MinLength(1, ErrorMessage = "Przynajmiej jeden wynik")]
        public List<ParticipantDto> Participations { get; set; } = new List<ParticipantDto>();
    }

    public class ParticipantDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "RacerId musi być większe od 0")]
        public int RacerId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Pozycja musi być większa niż 0")]
        public int Position { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "FinishTimeInSecond musi być większy niż 0")]
        public int FinishTimeInSeconds { get; set; }
    }
}