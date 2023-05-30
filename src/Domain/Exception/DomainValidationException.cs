using System.ComponentModel.DataAnnotations;

namespace Nexus.Domain;
public class DomainValidationException : ValidationException
{
    public DomainValidationException(string? message) : base(message)
    {
    }
}
