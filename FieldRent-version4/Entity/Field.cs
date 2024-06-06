namespace FieldRent.Entity;

public class Field
{
    public int FieldId { get; set; }
    public string? FieldCoordinate { get; set; }
    public int MapNumber { get; set; }


    /*public string? FieldImage { get; set; }
    public bool FieldIsActive { get; set; }*/


    public List<Map> Maps { get; set; } = new List<Map>();
}