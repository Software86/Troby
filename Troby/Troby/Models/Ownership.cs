using System;
using System.Collections.Generic;

namespace Troby.models
{
    public partial class Ownership
    {
        public int GameId { get; set; }
        public string UserId { get; set; }

        public virtual Games Game { get; set; }
        public virtual Users User { get; set; }
    }
}
