using CyclingTripManagement.Data;
using CyclingTripManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyclingTripManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParticipantsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ParticipantsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Participant>>> GetParticipants()
    {
        return await _context.Participants.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Participant>> GetParticipant(int id)
    {
        var participant = await _context.Participants
            .Include(p => p.TripRegistrations)
            .ThenInclude(tr => tr.Trip)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (participant == null)
        {
            return NotFound();
        }

        return participant;
    }

    [HttpPost]
    public async Task<ActionResult<Participant>> CreateParticipant(Participant participant)
    {
        _context.Participants.Add(participant);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetParticipant), new { id = participant.Id }, participant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateParticipant(int id, Participant participant)
    {
        if (id != participant.Id)
        {
            return BadRequest();
        }

        _context.Entry(participant).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ParticipantExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    private async Task<bool> ParticipantExists(int id)
    {
        return await _context.Participants.AnyAsync(e => e.Id == id);
    }
}
