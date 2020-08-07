namespace LT.DigitalOffice.FileService.Mappers.Interfaces
{
    public interface IMapper<TIn, TOut>
    {
        TOut Map(TIn item);
    }
}
