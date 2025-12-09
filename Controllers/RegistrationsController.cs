using CyclingTripManagement.Data;
using CyclingTripManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyclingTripManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RegistrationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripRegistration>>> GetRegistrations()
    {
        return await _context.TripRegistrations
            .Include(tr => tr.Trip)
            .Include(tr => tr.Participant)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TripRegistration>> GetRegistration(int id)
    {
        var registration = await _context.TripRegistrations
            .Include(tr => tr.Trip)
            .Include(tr => tr.Participant)
            .FirstOrDefaultAsync(tr => tr.Id == id);

        if (registration == null)
        {
            return NotFound();
        }

        return registration;
    }

    [HttpPost]
    public async Task<ActionResult<TripRegistration>> CreateRegistration(TripRegistration registration)
    {
        // Check if trip exists and has space
        var trip = await _context.Trips.FindAsync(registration.TripId);
        if (trip == null)
        {
            return BadRequest("Trip not found");
        }

        var currentRegistrations = await _context.TripRegistrations
            .CountAsync(tr => tr.TripId == registration.TripId && tr.Status != RegistrationStatus.Cancelled);

        if (currentRegistrations >= trip.MaxParticipants)
        {
            return BadRequest("Trip is fully booked");
        }

        // Calculate total price
        registration.TotalPrice = trip.BasePrice;
        if (registration.SingleRoomRequested)
        {
            registration.TotalPrice += trip.SingleRoomSupplement;
        }

        registration.RegistrationDate = DateTime.UtcNow;
        registration.Status = RegistrationStatus.Pending;

        _context.TripRegistrations.Add(registration);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRegistration), new { id = registration.Id }, registration);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRegistration(int id, TripRegistration registration)
    {
        if (id != registration.Id)
        {
            return BadRequest();
        }

        _context.Entry(registration).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await RegistrationExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelRegistration(int id)
    {
        var registration = await _context.TripRegistrations.FindAsync(id);
        if (registration == null)
        {
            return NotFound();
        }

        registration.Status = RegistrationStatus.Cancelled;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> RegistrationExists(int id)
    {
        return await _context.TripRegistrations.AnyAsync(e => e.Id == id);
    }
}
