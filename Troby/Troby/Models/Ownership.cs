using System;
using System.Collections.Generic;

namespace Troby.Models
{
    public partial class Ownership
    {
        public int GameId { get; set; }
        public string UserEmail { get; set; }
        public int Id { get; set; }

        public virtual Users UserEmailNavigation { get; set; }
    }
}
