using LT.DigitalOffice.Kernel.Broker;
using MassTransit;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ProjectService.Broker.Senders.Interfaces
{
    /// <summary>
    /// Broker sender interface.
    /// </summary>
    /// <typeparam name="TIn">Input type.</typeparam>
    /// <typeparam name="TOut">Output type.</typeparam>
    public interface ISender<TIn, TOut>
    {
        Task<Response<IOperationResult<TOut>>> GetResponseFromBroker(TIn item);
    }
}
