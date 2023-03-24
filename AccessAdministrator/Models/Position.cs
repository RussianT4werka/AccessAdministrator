using System;
using System.Collections.Generic;

namespace AccessAdministrator.Models
{
    public partial class Position
    {
        public Position()
        {
            UserWorkers = new HashSet<UserWorker>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<UserWorker> UserWorkers { get; set; }
    }
}
