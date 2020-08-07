<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.Mappers.Interfaces
{
    public interface IMapper<Tin, Tout>
    {
        Tout Map(Tin value);
=======
﻿namespace LT.DigitalOffice.ProjectService.Mappers.Interfaces
{
    public interface IMapper<TIn, TOut>
    {
        TOut Map(TIn item);
>>>>>>> develop
    }
}