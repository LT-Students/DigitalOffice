using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ProjectService.Models
{
    public class Project
    {
        public string Name { get; set; }
        public IEnumerable<Guid> ManagersIds { get; set; }
        public IEnumerable<Guid> WorkersIds { get; set; }
    }
}