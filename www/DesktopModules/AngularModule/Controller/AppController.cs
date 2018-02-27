using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularModule.Model;
using DotNetNuke.Data;

namespace AngularModule.Controller
{
    class AppController
    {
        /// <summary>
        /// The private field holding the instance 
        /// </summary>
        private static AppController _instance;

        /// <summary>
        /// Gets the instance (singleton pattern).
        /// </summary>
        /// <value>The instance.</value>
        public static AppController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppController();
                }
                return _instance;
            }
        }

        public ItemInfo GetItem(int ItemID)
        {
            ItemInfo item;
            string sql = "AngularModule_GetItem";
            using (IDataContext ctx = DataContext.Instance())
            {
                item = ctx.ExecuteSingleOrDefault<ItemInfo>(System.Data.CommandType.StoredProcedure, sql, ItemID);
            }
            return item;
        }

        public IEnumerable<ItemInfo> ListItems()
        {
            IEnumerable<ItemInfo> IEitems;
            string sql = "AngularModule_List";
            using (IDataContext ctx = DataContext.Instance())
            {
                IEitems = ctx.ExecuteQuery<ItemInfo>(System.Data.CommandType.StoredProcedure, sql);
            }
            return IEitems;
        }

        public int AddItem(ItemInfo objItemInfo)
        {
            int ItemID = 0;
            string sql = "AngularModule_Add";
            using (IDataContext ctx = DataContext.Instance())
            {
                ItemID = ctx.ExecuteScalar<int>(System.Data.CommandType.StoredProcedure, sql, objItemInfo.Title, objItemInfo.Description, objItemInfo.AssignedUserId, objItemInfo.ModuleId, objItemInfo.Sort, objItemInfo.CreatedOnDate, objItemInfo.CreatedByUserId, objItemInfo.LastModifiedOnDate, objItemInfo.LastModifiedByUserId);
            }
            return ItemID;
        }

        ///// <summary>
        ///// Gets the item.
        ///// </summary>
        ///// <param name="itemId">The item identifier.</param>
        ///// <param name="moduleId">The module identifier.</param>
        ///// <returns>ItemInfo.</returns>
        //public ItemInfo GetItem(int itemId)
        //{
        //    ItemInfo item;
        //    using (IDataContext ctx = DataContext.Instance())
        //    {
        //        var rep = ctx.GetRepository<ItemInfo>();
        //        item = rep.GetById(itemId);
        //    }
        //    return item;
        //}

        ///// <summary>
        ///// Creates a new item.
        ///// </summary>
        ///// <param name="item">The item.</param>
        ///// <returns>System.Int32.</returns>
        //public int NewItem(ItemInfo item)
        //{
        //    using (IDataContext ctx = DataContext.Instance())
        //    {
        //        var rep = ctx.GetRepository<ItemInfo>();
        //        rep.Insert((ItemInfo)item);
        //        return item.ItemId;
        //    }
        //}

        ///// <summary>
        ///// Updates the item.
        ///// </summary>
        ///// <param name="item">The item.</param>
        //public void UpdateItem(ItemInfo item)
        //{
        //    using (IDataContext ctx = DataContext.Instance())
        //    {
        //        var rep = ctx.GetRepository<ItemInfo>();
        //        rep.Update((ItemInfo)item);
        //    }
        //}

        ///// <summary>
        ///// Deletes the item.
        ///// </summary>
        ///// <param name="itemId">The item identifier.</param>
        //public void DeleteItem(int itemId)
        //{
        //    using (IDataContext ctx = DataContext.Instance())
        //    {
        //        string sql = "DELETE FROM {databaseOwner}[{objectQualifier}Angularmodule_Items] WHERE ItemId = @0";
        //        ctx.Execute(CommandType.Text, sql, itemId);
        //    }
        //}

        ///// <summary>
        ///// Sets the sort field of the item to an integer value corresponding to its display sort order
        ///// </summary>
        ///// <param name="itemId">The item identifier.</param>
        ///// <param name="moduleId">The module identifier.</param>
        //public void SetItemOrder(int itemId, int sort)
        //{
        //    using (IDataContext ctx = DataContext.Instance())
        //    {
        //        string sql = "UPDATE {databaseOwner}[{objectQualifier}Angularmodule_Items] SET Sort = @1 WHERE ItemId = @0";
        //        ctx.Execute(CommandType.Text, sql, itemId, sort);
        //    }
        //}
    }
}
