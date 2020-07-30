namespace LT.DigitalOffice.UserService.Mappers.Interfaces
{
    public interface IMapper<TIn, TOut>
    {
        TOut Map(TIn value);
    }
}
