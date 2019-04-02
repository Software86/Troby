using System;
using System.Collections.Generic;

namespace Troby.Models
{
    public partial class Achievements
    {
        public Achievements()
        {
            Unlocks = new HashSet<Unlocks>();
        }

        public enum Worth { bronze, silver, gold, platinum }

        public int Id { get; set; }
        public string Title { get; set; }
        public Worth worth { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
        public int GameId { get; set; }

        public virtual Games Game { get; set; }
        public virtual ICollection<Unlocks> Unlocks { get; set; }
    }
}
