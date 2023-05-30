using Common.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Nexus.Infrastructure.Configuration;
[SectionName("Database")]
public class DatabaseOptions : Option
{
    [Required]
    public string AppConnectionString { get; init; }

    [Required]
    public string HangfireConnectionString { get; init; }
}