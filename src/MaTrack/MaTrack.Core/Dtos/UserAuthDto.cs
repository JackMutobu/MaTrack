using MaTrack.Core.Enumerations;

namespace MaTrack.Core.Dtos
{
    public class UserAuthDto
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public EnumRole Role { get; set; }
    }
}
