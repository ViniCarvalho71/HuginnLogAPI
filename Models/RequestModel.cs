using System.ComponentModel.DataAnnotations;

namespace HuginnLogAPI.Models;

public class RequestModel
{
    public int Id { get; set; }
    [Required]
    public string Path { get; set; }
    [Required]
    public string Body { get; set; }
    [Required]
    public string Method { get; set; }
    
}