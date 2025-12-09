using System.ComponentModel.DataAnnotations;

namespace CyclingTripManagement.Models;

public class Participant
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateTime Birthday { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    [MaxLength(50)]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? StravaAccountLink { get; set; }

    [MaxLength(1000)]
    public string? SpecialDiets { get; set; }

    public bool SingleRoomRequest { get; set; }

    // Navigation property
    public ICollection<TripRegistration> TripRegistrations { get; set; } = new List<TripRegistration>();
}
