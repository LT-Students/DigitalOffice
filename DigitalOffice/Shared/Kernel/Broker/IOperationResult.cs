using System.Collections.Generic;

namespace LT.DigitalOffice.Kernel.Broker
{
    public interface IOperationResult<out T>
    {
        bool IsSuccess { get; }

        List<string> Errors { get; }

        T Body { get; }
    }
}