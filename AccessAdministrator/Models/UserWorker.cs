using System;
using System.Collections.Generic;

namespace AccessAdministrator.Models
{
    public partial class UserWorker
    {
        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string Gender { get; set; } = null!;
        public int PositionId { get; set; }
        public int? TypeId { get; set; }
        public string? SecretWord { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public byte[]? Photo { get; set; }

        public virtual Position Position { get; set; } = null!;
        public virtual Type? Type { get; set; }
    }
}
