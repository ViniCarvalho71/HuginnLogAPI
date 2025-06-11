using System.ComponentModel.DataAnnotations;

namespace HuginnLogAPI.Models;

public class User
{
    public long Id { get; set; }
    [Required, MaxLength(50)]
    public string Email { get; set; }
    [Required, MaxLength(100)]
    public string Password { get; set; }
    [Required, MaxLength(20)]
    public string Username { get; set; }
    [Required, MaxLength(50)]
    public string Name { get; set; }
    
    
}