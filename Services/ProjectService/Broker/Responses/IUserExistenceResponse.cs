namespace LT.DigitalOffice.ProjectService.Broker.Responses
{
    /// <summary>
    /// Interface for a respond that shows whether the user exists or not.
    /// </summary>
    public interface IUserExistenceResponse
    {
        bool Exists { get; }
    }
}
