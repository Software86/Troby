using System;
using System.Collections.Generic;

namespace Troby.models
{
    public partial class Games
    {
        public Games()
        {
            Achievements = new HashSet<Achievements>();
            Ownership = new HashSet<Ownership>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Bgg { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }

        public virtual ICollection<Achievements> Achievements { get; set; }
        public virtual ICollection<Ownership> Ownership { get; set; }
    }
}
