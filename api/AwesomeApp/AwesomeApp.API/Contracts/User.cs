using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AwesomeApp.API.Contracts;

[Table("users", Schema = "public")]
public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("identifier")]
    public Guid Identifier { get; set; }
    
    [Column("username")]
    public string UserName { get; set; }
    
    [Column("email")]
    public string Email { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    public static User Create(string username, string email)
    {
        return new User()
        {
            Identifier = Guid.NewGuid(),
            Email = email,
            UserName = username,
            CreatedAt = DateTime.Now
        };
    }
}