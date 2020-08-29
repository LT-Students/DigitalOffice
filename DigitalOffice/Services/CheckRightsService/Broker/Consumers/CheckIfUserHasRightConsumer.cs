using System.Threading.Tasks;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using MassTransit;

namespace LT.DigitalOffice.CheckRightsService.Broker.Consumers
{
    public class CheckIfUserHasRightConsumer : IConsumer<ICheckIfUserHasRightRequest>
    {
        private readonly ICheckRightsRepository repository;

        public CheckIfUserHasRightConsumer(ICheckRightsRepository repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<ICheckIfUserHasRightRequest> context)
        {
            await context.RespondAsync<IOperationResult<bool>>(
                OperationResultWrapper.CreateResponse(repository.CheckIfUserHasRight, context.Message));
        }
    }
}