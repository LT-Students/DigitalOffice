using System;
namespace UserService.Mappers.Interfaces
{
    public interface IMapper<TIn, TOut>
    {
        TOut Map(TIn value);
    }
}
