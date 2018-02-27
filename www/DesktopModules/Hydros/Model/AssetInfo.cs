using System;


namespace Hydros.Model
{
    public class AssetInfo
    {
        public string AssetID { get; set; }
        public string ItemID { get; set; }
        public string AssetType { get; set; }
        public string ItemType { get; set; }
        public int IsDeleted { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int IsPrimary { get; set;}
        public int Order { get; set; }
    }
}
