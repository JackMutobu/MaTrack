using System;
using System.ComponentModel.DataAnnotations;

namespace MaTrack.Core.Entities
{
    public abstract class  BasePersonEntity:BaseEntity
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public DateTime DoB { get; set; }
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        public string ProfileLink { get; set; }
    }
}
