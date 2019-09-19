using MaTrack.Core.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace MaTrack.Core.Entities
{
    public class UserEntity:BasePersonEntity
    {
        [Required]
        public string Password { get; set; }
        public EnumRole Role { get; set; }
    }
}
