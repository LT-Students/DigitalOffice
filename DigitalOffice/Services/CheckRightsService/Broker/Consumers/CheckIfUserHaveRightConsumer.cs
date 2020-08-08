using System.Threading.Tasks;
using LT.DigitalOffice.Broker.Requests;
using MassTransit;

namespace LT.DigitalOffice.CheckRightsService.Broker.Consumers
{
    public class CheckIfUserHaveRightConsumer : IConsumer<CheckIfUserHaveRightRequest>
    {
        public Task Consume(ConsumeContext<CheckIfUserHaveRightRequest> context)
        {
            throw new System.NotImplementedException();
        }
    }
}