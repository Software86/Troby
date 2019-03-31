using System;
using System.Collections.Generic;

namespace Troby.models
{
    public partial class Users
    {
        public Users()
        {
            Ownership = new HashSet<Ownership>();
            Tokens = new HashSet<Tokens>();
            Unlocks = new HashSet<Unlocks>();
        }

        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        public virtual ICollection<Ownership> Ownership { get; set; }
        public virtual ICollection<Tokens> Tokens { get; set; }
        public virtual ICollection<Unlocks> Unlocks { get; set; }
    }
}
