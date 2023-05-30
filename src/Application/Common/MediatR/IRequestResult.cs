namespace Nexus.Application.Common.MediatR;
public interface IRequestResult
{
    ResultStatus Status { get; set; }
}
