﻿using System;

namespace CheckRightsService.Database.Entities
{
    public class RightProjectLink
    {
        public Guid RightId { get; set; }
        public Right Right { get; set; }
        public Guid ProjectId { get; set; }
    }
}
