using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public byte Approved { get; set; }
        public byte CanAddData { get; set; }
        public byte CanViewData { get; set; }
        public byte CanReport { get; set; }

        public virtual Position Position { get; set; } = null!;
        public virtual Type? Type { get; set; }

        [NotMapped]
        public bool IsApproved
        {
            get => Approved == 1;
            set => Approved = value ? (byte)1 : (byte)0;
        }

        [NotMapped]
        public bool IsCanAddData
        {
            get => CanAddData == 1;
            set => CanAddData = value ? (byte)1 : (byte)0;
        }

        [NotMapped]
        public bool IsCanViewData
        {
            get => CanViewData == 1;
            set => CanViewData = value ? (byte)1 : (byte)0;
        }

        [NotMapped]
        public bool IsCanReport 
        { 
            get => CanReport == 1;
            set => CanReport = value ? (byte)1 : (byte)0; 
        }
    }
}
