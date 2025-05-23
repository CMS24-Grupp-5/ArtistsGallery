using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class Artists
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; } = null!;

    public string? UrlImage { get; set; }

    public string? Description { get; set; }

}
