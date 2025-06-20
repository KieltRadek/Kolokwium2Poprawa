using Kolokwium2.Data;
using Kolokwium2.DTOs;
using Kolokwium2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Controllers;

[ApiController]
[Route("api/racers")]
public class RacersController : ControllerBase
{
    private readonly AppDbContext _context;

    public RacersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}/participations")]
    public async Task<ActionResult<RacerParticipationsDto>> GetRacerParticipations(int id)
    {
        var racerExists = await _context.Racers.AnyAsync(r => r.RacerId == id);
        if (!racerExists)
        {
            return NotFound($"Zawodnik o ID {id} nie istnieje.");
        }

        var racer = await _context.Racers
            .Where(r => r.RacerId == id)
            .Select(r => new RacerParticipationsDto
            {
                RacerId = r.RacerId,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Participations = r.RaceParticipations.Select(rp => new ParticipationDto
                {
                    Race = new RaceDto
                    {
                        Name = rp.TrackRace.Race.Name,
                        Location = rp.TrackRace.Race.Location,
                        Date = rp.TrackRace.Race.Date
                    },
                    Track = new TrackDto
                    {
                        Name = rp.TrackRace.Track.Name,
                        LengthInKm = rp.TrackRace.Track.LengthInKm
                    },
                    Laps = rp.TrackRace.Laps,
                    FinishTimeInSeconds = rp.FinishTimeInSeconds,
                    Position = rp.Position
                }).ToList()
            }).FirstOrDefaultAsync();
        
        return Ok(racer);
    }
}