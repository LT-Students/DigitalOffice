using LT.DigitalOffice.Broker.Requests;

namespace LT.DigitalOffice.CheckRightsService.Commands.Interfaces
{
    public interface ICheckIfUserHaveRightCommand
    {
        bool Execute(ICheckIfUserHaveRightRequest request);
    }
}