using System;

namespace MaTrack.Core.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
