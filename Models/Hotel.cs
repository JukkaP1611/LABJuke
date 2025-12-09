using System.ComponentModel.DataAnnotations;

namespace CyclingTripManagement.Models;

public class Hotel
{
    public int Id { get; set; }

    [Required]
    public int TripId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Address { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? City { get; set; }

    [MaxLength(100)]
    public string? Country { get; set; }

    [Phone]
    [MaxLength(50)]
    public string? PhoneNumber { get; set; }

    [MaxLength(500)]
    public string? Website { get; set; }

    public int NightNumber { get; set; } // Which night of the trip

    // Navigation property
    public Trip Trip { get; set; } = null!;
}
