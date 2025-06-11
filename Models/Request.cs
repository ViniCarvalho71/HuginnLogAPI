using System.ComponentModel.DataAnnotations;

namespace HuginnLogAPI.Models;

public class Request
{
    public long Id { get; set; }
    [Required]
    public string Path { get; set; }
    [Required]
    public string Body { get; set; }
    [Required]
    public string Method { get; set; }
    [Required] 
    public DateTime DataCadastro { get; set; }
    
    public User User { get; set; }
}