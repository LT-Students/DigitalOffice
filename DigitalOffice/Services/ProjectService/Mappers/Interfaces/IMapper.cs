namespace LT.DigitalOffice.ProjectService.Mappers.Interfaces
{
    public interface IMapper<TIn, TOut>
    {
        TOut Map(TIn item);
    }
}