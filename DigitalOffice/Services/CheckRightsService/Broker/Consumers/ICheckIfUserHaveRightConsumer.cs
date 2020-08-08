using System.Threading.Tasks;
using LT.DigitalOffice.Broker.Requests;
using MassTransit;

namespace LT.DigitalOffice.CheckRightsService.Broker.Consumers
{
    public class CheckIfUserHaveRightConsumer : IConsumer<ICheckIfUserHaveRightRequest>
    {
        public Task Consume(ConsumeContext<ICheckIfUserHaveRightRequest> context)
        {
            throw new System.NotImplementedException();
        }
    }
}