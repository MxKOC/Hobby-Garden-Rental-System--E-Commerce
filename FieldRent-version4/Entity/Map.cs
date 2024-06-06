using System.ComponentModel.DataAnnotations;

namespace FieldRent.Entity;
public enum Duration
{
    Daily = 1,
    Weekly = 2,
    Mountly = 3,
    Yearly = 4
}
public class Map
{
    public int MapId { get; set; }
    public int MapPrice { get; set; }
    public string? MapCoordinate { get; set; }
    public string? MapCondition { get; set; } //Eğer nadas durumu varsa gerekebilir
    public DateTime? MapStart { get; set; }
    public DateTime? MapStop { get; set; }
    public bool MapIsActive { get; set; }

    public Duration? Time { get; set; }

    public int? UserId { get; set; }
    public User User { get; set; } = null!;

    public int? FieldId { get; set; }
    public Field Field { get; set; } = null!;

    public List<Request> Requests { get; set; } = new List<Request>();
}