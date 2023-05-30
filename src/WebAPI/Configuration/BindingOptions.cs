using Common.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Nexus.WebAPI.Configuration;

[SectionName("Binding")]
public sealed class BindingOptions : Option
{
    [Range(1024, ushort.MaxValue)]
    public ushort ApplicationPort { get; init; } = 4565;
    public IPAddress BindAddress { get; set; } = IPAddress.Any;
}