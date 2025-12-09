using System.ComponentModel.DataAnnotations;

namespace CyclingTripManagement.Models;

public class Trip
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    public int DurationDays { get; set; }

    public double AverageDailyDistance { get; set; } // in kilometers

    public double AverageDailyVerticalMeters { get; set; }

    public decimal BasePrice { get; set; }

    public decimal SingleRoomSupplement { get; set; }

    [MaxLength(500)]
    public string? StravaRouteLink { get; set; }

    [MaxLength(500)]
    public string? GpxFileUrl { get; set; }

    public int MaxParticipants { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation properties
    public ICollection<TripRegistration> TripRegistrations { get; set; } = new List<TripRegistration>();
    public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}
