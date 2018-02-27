using System;


namespace Hydros.Model
{
    public class AssetSummaryInfo
    {
        public string ItemID { get; set; }
        public string ItemType { get; set; }
        public string LatestModifiedDate { get; set; }
        public string Status { get; set; }
        public int TotalPhoto { get; set; }
        public int TotalVideo { get; set; }
        public int TotalDocument { get; set; }
    }
}
