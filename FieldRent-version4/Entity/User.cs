namespace FieldRent.Entity;
public class User
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    //public int UserPrice { get; set; }
    public bool IsAdmin { get; set; }

    public List<Map> Maps { get; set; } = new List<Map>();
    //null ları kaldır

}
