namespace MaTrack.Core.Entities
{
    public class AdminEntity:BasePersonEntity
    {
        public string SACCO { get; set; }
        public int? StageId { get; set; }
        public StageEntity Stage { get; set; }
    }
}
