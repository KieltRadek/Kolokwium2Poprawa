using Kolokwium2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kolokwium2.Models;
using Kolokwium2.DTOs;

namespace Kolokwium2.Controllers
{
    [ApiController]
    [Route("api/track-races")]
    public class TrackRacesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TrackRacesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("participants")]
        public async Task<IActionResult> AddParticipants([FromBody] AddRaceParticipationsRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var positionsInRequest = request.Participations.Select(p => p.Position).ToList();
            if (positionsInRequest.Count != positionsInRequest.Distinct().Count())
            {
                return BadRequest("Żądanie zawiera zduplikowane pozycje.");
            }

            var race = await _context.Races.FirstOrDefaultAsync(r => r.Name == request.RaceName);
            if (race == null) return NotFound($"Wyścig o nazwie '{request.RaceName}' nie istnieje.");

            var track = await _context.Tracks.FirstOrDefaultAsync(t => t.Name == request.TrackName);
            if (track == null) return NotFound($"Tor o nazwie '{request.TrackName}' nie istnieje.");

            var trackRace = await _context.TrackRaces
                .Include(tr => tr.RaceParticipations)
                .FirstOrDefaultAsync(tr => tr.RaceId == race.RaceId && tr.TrackId == track.TrackId);
            if (trackRace == null) return NotFound($"Nie znaleziono połączenia między wyścigiem '{request.RaceName}' a torem '{request.TrackName}'.");
            
            var requestedRacerIds = request.Participations.Select(p => p.RacerId).ToList();
            var existingRacerIds = await _context.Racers
                .Where(r => requestedRacerIds.Contains(r.RacerId))
                .Select(r => r.RacerId)
                .ToListAsync();

            var addedCount = 0;
            var updatedCount = 0;
            var skippedCount = 0;

            foreach (var dto in request.Participations)
            {
                if (!existingRacerIds.Contains(dto.RacerId))
                {
                    skippedCount++;
                    continue;
                }

                if (trackRace.RaceParticipations.Any(p => p.Position == dto.Position && p.RacerId != dto.RacerId))
                {
                    return BadRequest($"Konflikt: Pozycja {dto.Position} jest już zajęta przez innego zawodnika w tym wyścigu.");
                }

                var participation = trackRace.RaceParticipations.FirstOrDefault(p => p.RacerId == dto.RacerId);
                if (participation != null)
                {
                    participation.Position = dto.Position;
                    participation.FinishTimeInSeconds = dto.FinishTimeInSeconds;
                    updatedCount++;
                }
                else
                {
                    trackRace.RaceParticipations.Add(new RaceParticipation
                    {
                        RacerId = dto.RacerId,
                        Position = dto.Position,
                        FinishTimeInSeconds = dto.FinishTimeInSeconds
                    });
                    addedCount++;
                }
            }

            if (trackRace.RaceParticipations.Any())
            {
                var bestNewTime = trackRace.RaceParticipations.Min(p => p.FinishTimeInSeconds);
                if (trackRace.BestTimeInSeconds == null || bestNewTime < trackRace.BestTimeInSeconds)
                {
                    trackRace.BestTimeInSeconds = bestNewTime;
                }
            }
    
            await _context.SaveChangesAsync();
            
            var message = $"Operacja zakończona. Dodano: {addedCount}, zaktualizowano: {updatedCount}, pominięto (nieistniejący zawodnicy): {skippedCount}.";
            return Ok(new { message });
        }
    }
}