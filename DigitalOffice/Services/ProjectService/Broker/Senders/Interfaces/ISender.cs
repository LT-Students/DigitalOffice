using LT.DigitalOffice.Kernel.Broker;
using MassTransit;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ProjectService.Broker.Senders.Interfaces
{
    public interface ISender<TIn, TOut>
    {
        Task<Response<IOperationResult<TOut>>> GetResponseFromBroker(TIn item);
    }
}
