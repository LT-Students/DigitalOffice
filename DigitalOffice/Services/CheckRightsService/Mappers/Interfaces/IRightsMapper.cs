﻿namespace LT.DigitalOffice.CheckRightsService.Mappers.Interfaces
{
    public interface IMapper<Tin, Tout>
    {
        Tout Map(Tin value);
    }
}