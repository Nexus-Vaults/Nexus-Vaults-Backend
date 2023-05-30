using AutoMapper;

namespace Nexus.Application.Common;
public interface IMapFrom<T>
{
    void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
    }
}