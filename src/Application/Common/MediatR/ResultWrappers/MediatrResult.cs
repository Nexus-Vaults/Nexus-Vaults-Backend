namespace Nexus.Application.Common.MediatR;
public class MediatrResult
{
    public ResultStatus Status { get; }

    public MediatrResult(ResultStatus status)
    {
        Status = status;
    }
}
