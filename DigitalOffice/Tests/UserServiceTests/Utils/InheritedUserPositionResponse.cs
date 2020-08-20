using LT.DigitalOffice.Broker.Responses;

namespace LT.DigitalOffice.UserServiceUnitTests.Utils
{
    /// <summary>
    /// DTO for mass transit. Class allows testing where needed an instance of IUserPositionResponse.
    /// </summary>
    public class InheritedUserPositionResponse : IUserPositionResponse
    {
        public string UserPositionName { get; set; }
    }
}