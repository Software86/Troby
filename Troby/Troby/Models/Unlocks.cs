﻿using System;
using System.Collections.Generic;

namespace Troby.models
{
    public partial class Unlocks
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AchId { get; set; }
        public string UserId { get; set; }
        public int? Unlockscol { get; set; }

        public virtual Achievements Ach { get; set; }
        public virtual Users User { get; set; }
    }
}
