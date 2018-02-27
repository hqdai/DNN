using DotNetNuke.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydros.Model;

namespace Hydros.Controller
{
    class DatabaseController
    {
        /// <summary>
        /// The private field holding the instance 
        /// </summary>
        private static DatabaseController _instance;

        /// <summary>
        /// Gets the instance (singleton pattern).
        /// </summary>
        /// <value>The instance.</value>
        public static DatabaseController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseController();
                }
                return _instance;
            }
        }


        #region "Asset"
        public void Hydros_Assets_Add(AssetInfo Info)
        {
            string storename = "Hydros_Assets_Add";
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.Execute(System.Data.CommandType.StoredProcedure, storename,
                    Info.AssetID,
                    Info.ItemID,
                    Info.AssetType,
                    Info.ItemType,
                    Info.IsDeleted,
                    Info.DateAdded,
                    Info.DateModified,
                    Info.CreatedBy,
                    Info.ModifiedBy,
                    Info.IsPrimary,
                    Info.Order);
            }
        }

        public void Hydros_Assets_Update(AssetInfo Info)
        {
            string storename = "Hydros_Assets_Update";
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.Execute(System.Data.CommandType.StoredProcedure, storename,
                    Info.AssetID,
                    Info.ItemID,
                    Info.AssetType,
                    Info.ItemType,
                    Info.IsDeleted,
                    Info.DateAdded,
                    Info.DateModified,
                    Info.CreatedBy,
                    Info.ModifiedBy,
                    Info.IsPrimary,
                    Info.Order);
            }
        }

        public void Hydros_Assets_Update_Primary(AssetInfo Info)
        {
            string storename = "Hydros_Assets_Update_Primary";
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.Execute(System.Data.CommandType.StoredProcedure, storename,
                    Info.AssetID,
                    Info.ItemID,
                    Info.AssetType,
                    Info.DateModified,
                    Info.ModifiedBy);
            }
        }

        public void Hydros_Assets_Delete(string AssetID, int UserID)
        {
            DateTime CurrentTime = DateTime.Now;
            string storename = "Hydros_Assets_Delete";
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.Execute(System.Data.CommandType.StoredProcedure, storename,
                    AssetID,
                    CurrentTime,
                    UserID);
            }
        }

        public AssetInfo Hydros_Assets_Get(string AssetID)
        {
            string storename = "Hydros_Assets_Get";
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteScalar<AssetInfo>(System.Data.CommandType.StoredProcedure, storename, AssetID);
            }
        }

        public IEnumerable<AssetInfo> Hydros_Assets_List()
        {
            string storename = "Hydros_Assets_List";
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteQuery<AssetInfo>(System.Data.CommandType.StoredProcedure, storename);
            }
        }

        public IEnumerable<AssetInfo> Hydros_Assets_GetByItem(string ItemID)
        {
            string storename = "Hydros_Assets_GetByItem";
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteQuery<AssetInfo>(System.Data.CommandType.StoredProcedure, storename, ItemID);
            }
        }

        public IEnumerable<AssetPhotoInfo> Hydros_Assets_Photo_GetByItem(string ItemID)
        {
            string storename = "Hydros_Assets_Photo_GetByItem";
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteQuery<AssetPhotoInfo>(System.Data.CommandType.StoredProcedure, storename, ItemID);
            }
        }

        public IEnumerable<AssetDocumentInfo> Hydros_Assets_Document_GetByItem(string ItemID)
        {
            string storename = "Hydros_Assets_Document_GetByItem";
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteQuery<AssetDocumentInfo>(System.Data.CommandType.StoredProcedure, storename, ItemID);
            }
        }

        public IEnumerable<AssetSummaryInfo> Hydros_Assets_Summary(string FilterTerm, int SortIndex, string SortDirection, int StartRowNum, int EndRowNum)
        {
            string storename = "Hydros_Assets_Summary";
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteQuery<AssetSummaryInfo>(System.Data.CommandType.StoredProcedure, storename,
                    FilterTerm,
                    SortIndex,
                    SortDirection,
                    StartRowNum,
                    EndRowNum);
            }
        }

        public int Hydros_Assets_Summary_CountFilteredRow(string FilterTerm, int SortIndex, string SortDirection, int StartRowNum, int EndRowNum)
        {
            string storename = "Hydros_Assets_Summary_CountFilteredRow";
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteSingleOrDefault<int>(System.Data.CommandType.StoredProcedure, storename,
                    FilterTerm,
                    SortIndex,
                    SortDirection,
                    StartRowNum,
                    EndRowNum);
            }
        }

        public int Hydros_Assets_Summary_TotalRow()
        {
            string storename = "Hydros_Assets_Summary_TotalRow";
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteSingleOrDefault<int>(System.Data.CommandType.StoredProcedure, storename);
            }
        }


        #endregion

        #region "Cloudinary"
        public void Hydros_Cloudinary_Add(CloudinaryInfo Info)
        {
            string storename = "Hydros_Cloudinary_Add";
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.Execute(System.Data.CommandType.StoredProcedure, storename,
                    Info.PublicID,
                    Info.Version,
                    Info.Format,
                    Info.ResoureType,
                    Info.URI,
                    Info.SecureURI,
                    Info.Tags,
                    Info.Bytes,
                    Info.CreatedAt);
            }
        }
        #endregion

        #region "Data Definition"
        public string Hydros_GroupID_Get(string ItemID)
        {
            string storename = "Hydros_Def_Ref_GetGroupID_ByItem";
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteScalar<string>(System.Data.CommandType.StoredProcedure, storename, ItemID);
            }
        }
        #endregion

    }
}
