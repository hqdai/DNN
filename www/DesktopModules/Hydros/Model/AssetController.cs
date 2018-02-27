using System;
using Hydros.Controller;
using DotNetNuke.Entities.Users;
using System.Collections.Generic;

namespace Hydros.Model
{
    public class AssetController
    {
        public string AddItem(AssetInfo item)
        {
            try
            {
                DatabaseController.Instance.Hydros_Assets_Add(item);
                return item.AssetID;
            }
            catch
            {
                return "Fail";
            }
        }

        public IEnumerable<AssetInfo> List()
        {
            return DatabaseController.Instance.Hydros_Assets_List();
        }

        public AssetInfo Get(string AssetID)
        {
            return DatabaseController.Instance.Hydros_Assets_Get(AssetID);
        }

        public IEnumerable<AssetInfo> GetByItem(string ItemID)
        {
            return DatabaseController.Instance.Hydros_Assets_GetByItem(ItemID);
        }

        public IEnumerable<AssetPhotoInfo> PhotoGetByItem(string ItemID)
        {
            return DatabaseController.Instance.Hydros_Assets_Photo_GetByItem(ItemID);
        }

        //public IEnumerable<AssetSummaryInfo> Summary()
        //{
        //    return DatabaseController.Instance.Hydros_Assets_Summary();
        //}

    }
}
