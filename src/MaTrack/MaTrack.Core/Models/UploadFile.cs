using Microsoft.AspNetCore.Http;

namespace MaTrack.Core.Models
{
    public class UploadFile
    {
        public IFormFile File { get; set; }
    }
}
