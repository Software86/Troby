using System;
using System.Collections.Generic;

namespace Troby.models
{
    public partial class Tokens
    {
        public string TokenString { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Owner { get; set; }

        public virtual Users OwnerNavigation { get; set; }
    }
}
