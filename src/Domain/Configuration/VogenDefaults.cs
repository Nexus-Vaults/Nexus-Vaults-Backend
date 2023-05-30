using Nexus.Domain;
using Vogen;

[assembly: VogenDefaults(
    conversions: Conversions.EfCoreValueConverter | Conversions.SystemTextJson | Conversions.TypeConverter,
    throws: typeof(DomainValidationException)
)]

