using System;
using System.Collections.Generic;

namespace Troby.models
{
    public partial class Achievements
    {
        public Achievements()
        {
            Unlocks = new HashSet<Unlocks>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Worth { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
        public int GameId { get; set; }

        public virtual Games Game { get; set; }
        public virtual ICollection<Unlocks> Unlocks { get; set; }
    }
}
