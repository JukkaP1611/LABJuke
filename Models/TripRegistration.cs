using System.ComponentModel.DataAnnotations;

namespace CyclingTripManagement.Models;

public class TripRegistration
{
    public int Id { get; set; }

    [Required]
    public int TripId { get; set; }

    [Required]
    public int ParticipantId { get; set; }

    [Required]
    public DateTime RegistrationDate { get; set; }

    public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;

    public bool SingleRoomRequested { get; set; }

    public decimal TotalPrice { get; set; }

    // Navigation properties
    public Trip Trip { get; set; } = null!;
    public Participant Participant { get; set; } = null!;
}

public enum RegistrationStatus
{
    Pending,
    Confirmed,
    Cancelled
}
